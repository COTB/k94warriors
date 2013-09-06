using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class WarriorInfoMap : EntityTypeConfiguration<WarriorInfo>
    {
        public WarriorInfoMap()
        {
            // Primary Key
            HasKey(t => t.WarriorID);

            // Properties
            Property(t => t.FirstName)
                .HasMaxLength(50);

            Property(t => t.LastName)
                .HasMaxLength(50);

            Property(t => t.Phone)
                .HasMaxLength(30);

            Property(t => t.Email)
                .HasMaxLength(200);

            Property(t => t.Address)
                .HasMaxLength(200);

            Property(t => t.City)
                .HasMaxLength(100);

            Property(t => t.State)
                .HasMaxLength(2);

            Property(t => t.Zip)
                .HasMaxLength(10);

            // Table & Column Mappings
            ToTable("WarriorInfo");
            Property(t => t.WarriorID).HasColumnName("WarriorID");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.Zip).HasColumnName("Zip");
            Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
        }
    }
}