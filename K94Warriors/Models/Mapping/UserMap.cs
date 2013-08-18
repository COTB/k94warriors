using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.DisplayName)
                .HasMaxLength(200);

            this.Property(t => t.PhoneProvider)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            this.Property(t => t.PhoneProvider).HasColumnName("PhoneProvider");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
        }
    }
}
