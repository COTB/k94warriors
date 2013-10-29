using K94Warriors.Data;
using K94Warriors.Models;
using System.Data.Entity.Migrations;

namespace K94Warriors.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<K9DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(K9DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.UserTypes.AddOrUpdate(i => i.ID,
                new UserType { ID = 1, Name = "Administrator"},
                new UserType { ID = 2, Name = "Trainer" },
                new UserType { ID = 3, Name = "Volunteer" }
            );

            context.NoteTypes.AddOrUpdate(i => i.Name,
                new NoteType { Name = "General Note" },
                new NoteType { Name = "Training Feedback" }
            );

            context.EventTypes.AddOrUpdate(i => i.Name,
                new EventType { Name = "General Event" },
                new EventType { Name = "Vet Appointment" }
            );

            context.MedicalRecordTypes.AddOrUpdate(i => i.Name,
                new MedicalRecordType { Name = "Miscellaneous" },
                new MedicalRecordType { Name = "Vaccination" }
            );
        }
    }
}
