using System.Data.Entity;
using K94Warriors.Models;

namespace K94Warriors.Data
{
    public class K9DbContext : DbContext
    {
        public K9DbContext()
            : this("K9")
        {
        }

        public K9DbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }
        
        public DbSet<DogEvent> DogEvents { get; set; }

        public DbSet<DogImage> DogImages { get; set; }

        public DbSet<DogMedicalRecord> DogMedicalRecords { get; set; }

        public DbSet<DogMedicalRecordImage> DogMedicalRecordImages { get; set; }

        public DbSet<DogMedication> DogMedications { get; set; }

        public DbSet<DogNote> DogNotes { get; set; }
        
        public DbSet<DogProfile> DogProfiles { get; set; }
        
        public DbSet<DogSkillName> DogSkillNames { get; set; }
        
        public DbSet<DogSkill> DogSkills { get; set; }
        
        public DbSet<EventType> EventTypes { get; set; }
        
        public DbSet<Location> Locations { get; set; }
        
        public DbSet<NoteType> NoteTypes { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserType> UserTypes { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {   
            
        }
    }
}