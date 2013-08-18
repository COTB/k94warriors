using System.Data.Entity;
using K94Warriors.Models;

namespace K94Warriors
{
    public class K9DbContext : DbContext
    {
        public K9DbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<DogEvent> DogEvents { get; set; }

        public DbSet<DogMedicalRecord> DogMedicalRecords { get; set; }

        public DbSet<DogNote> DogNotes { get; set; }

        public DbSet<DogProfile> DogProfiles { get; set; }

        public DbSet<DogSkill> DogSkills { get; set; }

        public DbSet<DogSkillName> DogSkillNames { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<WarriorInfo> WarriorInfos { get; set; }
    }
}