using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Models.Mapping
{
    public class DogSkillMap : EntityTypeConfiguration<DogSkill>
    {
        public DogSkillMap()
        {
            // Primary Key
            this.HasKey(t => t.DogSkilID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DogSkills");
            this.Property(t => t.DogSkilID).HasColumnName("DogSkilID");
            this.Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.SkillNameId).HasColumnName("SkillNameId");

            // Relationships
            this.HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogSkills)
                .HasForeignKey(d => d.DogProfileID);
            this.HasRequired(t => t.DogSkillName)
                .WithMany(t => t.DogSkills)
                .HasForeignKey(d => d.SkillNameId);

        }
    }
}
