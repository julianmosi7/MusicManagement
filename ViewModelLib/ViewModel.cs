using DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLib
{
    public class ViewModel
    {
        public ViewModel() { }

        private MusicContext db;
        public ViewModel(MusicContext db)
        {
            this.db = db;
            Artists = db.Artists.ToList();            
        }

        public List<Artist> Artists { get; private set; }

    }
}
