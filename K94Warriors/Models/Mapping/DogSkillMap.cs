using System.Data.Entity.ModelConfiguration;

namespace K94Warriors.Models.Mapping
{
    public class DogSkillMap : EntityTypeConfiguration<DogSkill>
    {
        public DogSkillMap()
        {
            // Primary Key
            HasKey(t => t.DogSkilID);

            // Properties
            // Table & Column Mappings
            ToTable("DogSkills");
            Property(t => t.DogSkilID).HasColumnName("DogSkilID");
            Property(t => t.DogProfileID).HasColumnName("DogProfileID");
            Property(t => t.Level).HasColumnName("Level");
            Property(t => t.SkillNameId).HasColumnName("SkillNameId");

            // Relationships
            HasRequired(t => t.DogProfile)
                .WithMany(t => t.DogSkills)
                .HasForeignKey(d => d.DogProfileID);
            HasRequired(t => t.DogSkillName)
                .WithMany(t => t.DogSkills)
                .HasForeignKey(d => d.SkillNameId);
        }
    }
}