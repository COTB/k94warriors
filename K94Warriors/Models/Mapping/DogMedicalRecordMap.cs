using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogMedicalRecordMap : EntityTypeConfiguration<DogMedicalRecord>
    {
        public DogMedicalRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.RecordType)
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .HasMaxLength(200);

            this.Property(t => t.RecordURL)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("DogMedicalRecords");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            this.Property(t => t.RecordType).HasColumnName("RecordType");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.RecordExpirationDate).HasColumnName("RecordExpirationDate");
            this.Property(t => t.RecordURL).HasColumnName("RecordURL");

            // Relationships
            this.HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogMedicalRecords)
                .HasForeignKey(d => d.DogProfileID);

        }
    }
}
