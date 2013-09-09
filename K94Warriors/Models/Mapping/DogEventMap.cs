using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogEventMap : EntityTypeConfiguration<DogEvent>
    {
        public DogEventMap()
        {
            // Primary Key
            HasKey(t => t.EventID);

            // Properties
            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("DogEvents");
            Property(t => t.EventID).HasColumnName("EventID");
            Property(t => t.Subject).HasColumnName("Subject");
            Property(t => t.Body).HasColumnName("Body");
            Property(t => t.IsComplete).HasColumnName("IsComplete");
            Property(t => t.EventDate).HasColumnName("EventDate");
            Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            Property(t => t.EventTypeId).HasColumnName("EventTypeId");

            // Relationships
            HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogEvents)
                .HasForeignKey(d => d.DogProfileID);
            HasRequired(t => t.EventType)
                .WithMany(t => t.DogEvents)
                .HasForeignKey(d => d.EventTypeId);
        }
    }
}