using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogNoteMap : EntityTypeConfiguration<DogNote>
    {
        public DogNoteMap()
        {
            // Primary Key
            HasKey(t => t.NoteID);

            // Properties
            // Table & Column Mappings
            ToTable("DogNotes");
            Property(t => t.NoteID).HasColumnName("NoteID");
            Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            Property(t => t.Note).HasColumnName("Note");
            Property(t => t.IsCritical).HasColumnName("IsCritical");
            Property(t => t.NoteTypeId).HasColumnName("NoteTypeId");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");

            // Relationships
            HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(d => d.DogProfileID);

            HasRequired(t => t.NoteType)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(d => d.NoteTypeId);

            HasRequired(t => t.User)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(t => t.CreatedByUserId);
        }
    }
}