using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Outlook_Notification_Service
{
    public class NotificationConfigration : EntityTypeConfiguration<Models.Notification>
    {
        public NotificationConfigration()
        {
            ToTable("Notifications");

            HasKey(m => m.Id);

            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }


    public class DefaultDbConext : DbContext
    {
        public DefaultDbConext() : base("DefaultConnection")
        {

        }

        public DbSet<Models.Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NotificationConfigration());

            base.OnModelCreating(modelBuilder);
        }
    }
}