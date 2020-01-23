using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public class MusicContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordType> RecordTypes { get; set; }
        public DbSet<Song> Songs { get; set; }

        public MusicContext() { }
        public MusicContext(string NameOrConnectionString) : base("MusicContext") { }
    }
}
