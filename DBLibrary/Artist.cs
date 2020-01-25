using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public partial class Artist
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }

        public virtual List<Record> Records { get; set; }
    }
}
