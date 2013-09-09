using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogMedicalRecordMap : EntityTypeConfiguration<DogMedicalRecord>
    {
        public DogMedicalRecordMap()
        {
            // Primary Key
            HasKey(t => t.RecordID);

            // Properties
            Property(t => t.RecordType)
                .HasMaxLength(200);

            Property(t => t.Title)
                .HasMaxLength(200);

            Property(t => t.RecordURL)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("DogMedicalRecords");
            Property(t => t.RecordID).HasColumnName("RecordID");
            Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            Property(t => t.RecordType).HasColumnName("RecordType");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.RecordExpirationDate).HasColumnName("RecordExpirationDate");
            Property(t => t.RecordURL).HasColumnName("RecordURL");

            // Relationships
            HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogMedicalRecords)
                .HasForeignKey(d => d.DogProfileID);
        }
    }
}