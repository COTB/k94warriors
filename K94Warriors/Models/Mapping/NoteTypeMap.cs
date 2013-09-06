using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class NoteTypeMap : EntityTypeConfiguration<NoteType>
    {
        public NoteTypeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("NoteTypes");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}