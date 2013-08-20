using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogNoteMap : EntityTypeConfiguration<DogNote>
    {
        public DogNoteMap()
        {
            // Primary Key
            this.HasKey(t => t.NoteID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DogNotes");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            this.Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsCritical).HasColumnName("IsCritical");
            this.Property(t => t.NoteTypeId).HasColumnName("NoteTypeId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedByUserId).HasColumnName("CreatedByUserId");

            // Relationships
            this.HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(d => d.DogProfileID);

            this.HasRequired(t => t.NoteType)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(d => d.NoteTypeId);

            this.HasRequired(t => t.User)
                .WithMany(t => t.DogNotes)
                .HasForeignKey(t => t.CreatedByUserId);

        }
    }
}
