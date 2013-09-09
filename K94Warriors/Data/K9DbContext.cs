using System.Data.Entity;
using K94Warriors.Models;
using K94Warriors.Models.Mapping;

namespace K94Warriors.Data
{
    public class K9DbContext : DbContext
    {
        public K9DbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }


        public DbSet<DogEvent> DogEvents { get; set; }
        public DbSet<DogMedicalRecord> DogMedicalRecords { get; set; }
        public DbSet<DogNote> DogNotes { get; set; }
        public DbSet<DogProfile> DogProfiles { get; set; }
        public DbSet<DogSkillName> DogSkillNames { get; set; }
        public DbSet<DogSkill> DogSkills { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<WarriorInfo> WarriorInfoes { get; set; }
        public DbSet<DogEventsReport> DogEventsReports { get; set; }
        public DbSet<DogNotesReport> DogNotesReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DogEventMap());
            modelBuilder.Configurations.Add(new DogMedicalRecordMap());
            modelBuilder.Configurations.Add(new DogNoteMap());
            modelBuilder.Configurations.Add(new DogProfileMap());
            modelBuilder.Configurations.Add(new DogSkillNameMap());
            modelBuilder.Configurations.Add(new DogSkillMap());
            modelBuilder.Configurations.Add(new EventTypeMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new NoteTypeMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
            modelBuilder.Configurations.Add(new WarriorInfoMap());
            modelBuilder.Configurations.Add(new DogEventsReportMap());
            modelBuilder.Configurations.Add(new DogNotesReportMap());
        }
    }
}