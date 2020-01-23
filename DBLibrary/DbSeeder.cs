using DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public static class DbSeeder
    {
        public static MusicContext SeedIfEmpty(this MusicContext db)
        {
            AssertDatabase(db);
            if (db.Artists.Any()) return db;
            Seed(db);
            db.SaveChanges();
            return db;
        }

        public static void Seed(MusicContext db)
        {
            string csv_file = System.IO.File.ReadAllText("musicDbData_small.csv");
            csv_file = csv_file.Replace('\n', '\r');
            Console.WriteLine(csv_file);
        }

        private static void AssertDatabase(MusicContext db)
        {
            /*
            Console.WriteLine("----------AssertDatabase----------");
            bool dbExists = db.Database.Exists();
            if (dbExists)
            {
                Console.WriteLine($"Database exists: {db.Database.Connection.ConnectionString}");
                bool dbStructureOk = db.Database.CompatibleWithModel(true);
                Console.WriteLine($"STructure still the same? {dbStructureOk}");
                if (dbStructureOk) return;

                Console.WriteLine("Delete the database");
                db.Database.Delete();
            }
            Console.WriteLine("Create the database with actual configuration");
            db.Database.Create();
            */
            if (db.Database.Exists())
            {
                if (db.Database.CompatibleWithModel(true)) return;
                db.Database.Delete();
            }
            db.Database.Create();
        }
    }
}
