using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogNotesReportMap : EntityTypeConfiguration<DogNotesReport>
    {
        public DogNotesReportMap()
        {
            // Primary Key
            HasKey(t => new {t.NoteID, t.DogProfileID, t.IsCritical, t.IsFixed, t.CreatedTimeUTC, t.CreatedByUserID});

            // Properties
            Property(t => t.NoteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.DogProfileID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Name)
                .HasMaxLength(200);

            Property(t => t.Breed)
                .HasMaxLength(200);

            Property(t => t.Color)
                .HasMaxLength(200);

            Property(t => t.FirstName)
                .HasMaxLength(50);

            Property(t => t.LastName)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("DogNotesReport");
            Property(t => t.NoteID).HasColumnName("NoteID");
            Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            Property(t => t.Note).HasColumnName("Note");
            Property(t => t.IsCritical).HasColumnName("IsCritical");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Breed).HasColumnName("Breed");
            Property(t => t.Age).HasColumnName("Age");
            Property(t => t.Color).HasColumnName("Color");
            Property(t => t.PickedUpDate).HasColumnName("PickedUpDate");
            Property(t => t.DonorID).HasColumnName("DonorID");
            Property(t => t.IsFixed).HasColumnName("IsFixed");
            Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            Property(t => t.SponsorID).HasColumnName("SponsorID");
            Property(t => t.WarriorID).HasColumnName("WarriorID");
            Property(t => t.CreatedTimeUTC).HasColumnName("CreatedTimeUTC");
            Property(t => t.CreatedByUserID).HasColumnName("CreatedByUserID");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
        }
    }
}