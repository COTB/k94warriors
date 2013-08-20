using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogProfileMap : EntityTypeConfiguration<DogProfile>
    {
        public DogProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.ProfileID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Breed)
                .HasMaxLength(200);

            this.Property(t => t.Color)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DogProfiles");
            this.Property(t => t.ProfileID).HasColumnName("ProfileID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Breed).HasColumnName("Breed");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.PickedUpDate).HasColumnName("PickedUpDate");
            this.Property(t => t.IsFixed).HasColumnName("IsFixed");
            this.Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            this.Property(t => t.WarriorID).HasColumnName("WarriorID");
            this.Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");
            this.Property(t => t.LocationId).HasColumnName("LocationId");

            // Relationships
            this.HasOptional(t => t.WarriorInfo)
                .WithMany(t => t.DogProfiles)
                .HasForeignKey(d => d.WarriorID);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.DogProfiles)
                .HasForeignKey(d => d.LocationId);

        }
    }
}
