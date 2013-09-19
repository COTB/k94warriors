using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogProfileMap : EntityTypeConfiguration<DogProfile>
    {
        public DogProfileMap()
        {
            // Primary Key
            HasKey(t => t.ProfileID);

            // Properties
            Property(t => t.Name)
                .HasMaxLength(200);

            Property(t => t.Breed)
                .HasMaxLength(200);

            Property(t => t.Color)
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("DogProfiles");
            Property(t => t.ProfileID).HasColumnName("ProfileID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Breed).HasColumnName("Breed");
            Property(t => t.BirthYear).HasColumnName("BirthYear");
            Property(t => t.Color).HasColumnName("Color");
            Property(t => t.Gender).HasColumnName("Gender");
            Property(t => t.PickedUpDate).HasColumnName("PickedUpDate");
            Property(t => t.IsFixed).HasColumnName("IsFixed");
            Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            Property(t => t.WarriorID).HasColumnName("WarriorID");
            Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            Property(t => t.IsApproved).HasColumnName("IsApproved");
            Property(t => t.LocationId).HasColumnName("LocationId");

            // Relationships
            HasOptional(t => t.WarriorInfo)
                .WithMany(t => t.DogProfiles)
                .HasForeignKey(d => d.WarriorID);
            HasRequired(t => t.Location)
                .WithMany(t => t.DogProfiles)
                .HasForeignKey(d => d.LocationId);
        }
    }
}