using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogEventMap : EntityTypeConfiguration<DogEvent>
    {
        public DogEventMap()
        {
            // Primary Key
            this.HasKey(t => t.EventID);

            // Properties
            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DogEvents");
            this.Property(t => t.EventID).HasColumnName("EventID");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsComplete).HasColumnName("IsComplete");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            this.Property(t => t.EventTypeId).HasColumnName("EventTypeId");

            // Relationships
            this.HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogEvents)
                .HasForeignKey(d => d.DogProfileID);
            this.HasRequired(t => t.EventType)
                .WithMany(t => t.DogEvents)
                .HasForeignKey(d => d.EventTypeId);

        }
    }
}
