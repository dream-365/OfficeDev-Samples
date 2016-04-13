using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console_with_O365
{
    public class InMemoryTokenCache : TokenCache
    {
        private static ReaderWriterLockSlim SessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        string UserObjectId = string.Empty;

        string CacheId = string.Empty;

        private static IDictionary<string, byte[]> CACHES = new Dictionary<string, byte[]>();

        public InMemoryTokenCache(string userId)
        {
            UserObjectId = userId;

            CacheId = UserObjectId + "_TokenCache";

            this.AfterAccess = AfterAccessNotification;

            this.BeforeAccess = BeforeAccessNotification;

            Load();
        }

        public void Summary()
        {
            Console.WriteLine("cache count: {0}", CACHES.Count);
        }

        public void Load()
        {
            SessionLock.EnterReadLock();
            this.Deserialize(CACHES.ContainsKey(CacheId) ? CACHES[CacheId] : null);
            SessionLock.ExitReadLock();
        }

        public void Persist()
        {
            SessionLock.EnterWriteLock();

            this.HasStateChanged = false;

            CACHES[CacheId] = this.Serialize();

            SessionLock.ExitWriteLock();
        }

        // Empties the persistent store.
        public override void Clear()
        {
            base.Clear();

            CACHES.Remove(CacheId);
        }

        // Triggered right before ADAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            Load();
        }

        // Triggered right after ADAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (this.HasStateChanged)
            {
                Persist();
            }
        }
    }
}
