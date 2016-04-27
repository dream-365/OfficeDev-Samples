using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AspNetMvc_MultiTenant
{
    public class TokenCacheDbContext : DbContext
    {
        public TokenCacheDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserTokenCache> UserTokenCaches { get; set; }
    }


    public class UserTokenCache
    {
        [Key]
        public string WebUserUniqueId { get; set; }

        public byte[] CacheBits { get; set; }

        public DateTime LastWrite { get; set; }
    }
}