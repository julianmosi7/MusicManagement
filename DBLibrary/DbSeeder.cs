using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public static class DbSeeder
    {
        public static MusicContext SeedIfEmpty(this MusicContext db)
        {
            //AssertDatabase(db);   
            Delete(db);
            //if (db.Artists.Any()) return db;
            //Console.WriteLine("Db is not Empty");
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
                string recordType = parts[2];
                string artistName = parts[0];
                string recordTitle = parts[1];
                string year = parts[3];
                string songTitle = parts[4];

                var recordTypeA = db.RecordTypes.Where(x => x.Descr.Equals(recordType)).FirstOrDefault();
                if (recordTypeA == null)
                {
                    recordTypeA = new RecordType { Descr = recordType };
                    db.RecordTypes.Add(recordTypeA);
                    db.SaveChanges();
                }

                var artistA = db.Artists.Where(x => x.ArtistName.Equals(artistName)).FirstOrDefault();
                if(artistA == null)
                {
                    artistA = new Artist { ArtistName = artistName };
                    db.Artists.Add(artistA);
                    db.SaveChanges();
                }


                var recordA = db.Records.Where(x => x.RecordTitle.Equals(recordTitle)).FirstOrDefault();
                if(recordA == null)
                {
                    recordA = new Record { RecordTitle = recordTitle, Year = year, Artist = artistA, RecordType = recordTypeA };
                    db.Records.Add(recordA);
                    db.SaveChanges();
                }
                    
                var songA = db.Songs.Where(x => x.SongTitle.Equals(songTitle)).FirstOrDefault();
                if(songA == null)
                {
                    songA = new Song { SongTitle = songTitle, Record = recordA };
                    db.Songs.Add(songA);
                    db.SaveChanges();
                }             
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
            if (db.Database.Exists())
            {
                if (db.Database.CompatibleWithModel(true)) return;
                db.Database.Delete();
            }
            db.Database.Create();
        }    
        
        private static void Delete(MusicContext db)
        {
            db.Database.Delete();
            db.Database.Create();
        }
    }
}

