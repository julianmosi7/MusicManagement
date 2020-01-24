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
            Console.WriteLine("Db is not Empty");
            Seed(db);
            db.SaveChanges();
            return db;
        }

        public static void Seed(MusicContext db)
        {
            string[] csv_file = GetFile();
            for (int i = 1; i < csv_file.Length; i++)
            {
                string line = csv_file[i];
                string[] parts = line.Split(';');

                var recordTypeA = new RecordType { Descr = parts[2] };
                var artistA = new Artist { ArtistName = parts[0] };
                var recordA = new Record { RecordTitle = parts[1], Year = parts[3], Artist = artistA, RecordType = recordTypeA};
                var songA = new Song { SongTitle = parts[4], Record = recordA};
                db.Artists.Add(artistA);
                db.Records.Add(recordA);
                db.RecordTypes.Add(recordTypeA);
                db.Songs.Add(songA);
            }
        }

        public static string[] GetFile()
        {
            string[] csv_file = System.IO.File.ReadAllLines("musicDbData_small.csv");
            Console.WriteLine(csv_file);
            return csv_file;
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
                Console.WriteLine($"Structure still the same? {dbStructureOk}");
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
