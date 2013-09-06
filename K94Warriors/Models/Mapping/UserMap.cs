using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.UserID);

            // Properties
            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(300);

            Property(t => t.Phone)
                .HasMaxLength(30);

            Property(t => t.DisplayName)
                .HasMaxLength(200);

            Property(t => t.PhoneProvider)
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("Users");
            Property(t => t.UserID).HasColumnName("UserID");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.DisplayName).HasColumnName("DisplayName");
            Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            Property(t => t.PhoneProvider).HasColumnName("PhoneProvider");
            Property(t => t.UserTypeID).HasColumnName("UserTypeId");

            HasRequired(t => t.UserType)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.UserTypeID);
        }
    }
}