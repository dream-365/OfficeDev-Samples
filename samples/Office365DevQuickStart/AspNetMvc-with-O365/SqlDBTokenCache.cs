using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Data.Entity;
using System.Linq;

namespace AspNetMvc_with_O365
{
    public class SqlDBTokenCache : TokenCache
    {
        private TokenCacheDbContext db = new TokenCacheDbContext();

        private UserTokenCache _cache;

        private string _userUniqueId { get; set; }

        public void EnsureHasCacheMatched()
        {
            if(_cache == null)
            {
                throw new Office365AssertFailedException("Thre is not token matched in database");
            }
        }


        public SqlDBTokenCache(string userUniqueId)
        {
            AfterAccess = AfterAccessNotification;

            BeforeAccess = BeforeAccessNotification;

            _userUniqueId = userUniqueId;

            LoadCacheFromDatabase();
        }

        private void LoadCacheFromDatabase()
        {
            _cache = db.UserTokenCaches.FirstOrDefault(c => c.WebUserUniqueId == _userUniqueId);

            Deserialize((_cache == null) ? null : _cache.CacheBits);
        }

        public override void Clear()
        {
            base.Clear();

            foreach (var cacheEntry in db.UserTokenCaches.Where(m => m.WebUserUniqueId == _userUniqueId))
            {
                db.UserTokenCaches.Remove(cacheEntry);
            }
                
            db.SaveChanges();
        }

        private void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            if (_cache == null)
            {
                _cache = db.UserTokenCaches.FirstOrDefault(m => m.WebUserUniqueId == _userUniqueId);
            }
            else
            {
                var cacheInDatabase = db.UserTokenCaches.FirstOrDefault(m => m.WebUserUniqueId == _userUniqueId);

                if(cacheInDatabase != null && cacheInDatabase.LastWrite > _cache.LastWrite)
                {
                    _cache = cacheInDatabase;
                }
            }

            Deserialize((_cache == null) ? null : _cache.CacheBits);
        }

        private void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            if (HasStateChanged)
            {
                var cacheInDatabase = db.UserTokenCaches.FirstOrDefault(m => m.WebUserUniqueId == _userUniqueId);

                if (cacheInDatabase != null)
                {
                    cacheInDatabase.CacheBits = Serialize();

                    cacheInDatabase.LastWrite = DateTime.Now;
                }
                else
                {
                    db.UserTokenCaches.Add(new UserTokenCache
                    {
                        WebUserUniqueId = _userUniqueId,
                        CacheBits = Serialize(),
                        LastWrite = DateTime.Now
                    });
                }

                db.SaveChanges();

                HasStateChanged = false;
            }
        }
    }
}