using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogEventsReportMap : EntityTypeConfiguration<DogEventsReport>
    {
        public DogEventsReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EventID, t.Subject, t.IsComplete, t.EventDate, t.DogProfileID, t.IsFixed, t.CreatedTimeUTC, t.CreatedByUserID });

            // Properties
            this.Property(t => t.EventID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DogProfileID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Breed)
                .HasMaxLength(200);

            this.Property(t => t.Color)
                .HasMaxLength(200);

            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DogEventsReport");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsComplete).HasColumnName("IsComplete");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Breed).HasColumnName("Breed");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.PickedUpDate).HasColumnName("PickedUpDate");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.IsFixed).HasColumnName("IsFixed");
            this.Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            this.Property(t => t.SponsorID).HasColumnName("SponsorID");
            this.Property(t => t.WarriorID).HasColumnName("WarriorID");
            this.Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            this.Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
        }
    }
}
