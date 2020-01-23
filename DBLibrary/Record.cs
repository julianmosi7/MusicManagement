using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public class Record
    {
        public int Id { get; set; }
        public string RecordTitle { get; set; }
        public string Year { get; set; }
        public int ArtistId { get; set; }
        public int RecordTypeId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual RecordType RecordType { get; set; }
        public virtual List<Song> Songs { get; set; }
    }
}
