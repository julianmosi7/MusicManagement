using DBLib;
using MVVM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLib
{
    public class ViewModel : ObservableObject
    {       
        public ViewModel() { }

        private MusicContext db;

        public ViewModel(MusicContext db)
        {
            this.db = db;
            Artists = db.Artists.ToList();           
        }

        private List<Artist> artists;

        public List<Artist> Artists
        {
            get { return artists; }
            set
            {
                artists = value;
                Interpreten = db.Artists.Count();                
            }
        }
        

        private Artist selectedArtist;

        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                selectedArtist = value;
                Artists = db.Artists.Where(x => x.ArtistName == selectedArtist.ArtistName).ToList();
                Records = db.Records.Where(x => x.Artist.ArtistName == selectedArtist.ArtistName).ToList();
                
                RaisePropertyChangedEvent(nameof(SelectedArtist));
            }
        }


        private double interpreten;

        public double Interpreten

        {
            get { return interpreten; }
            set
            {
                interpreten = value;
                RaisePropertyChangedEvent(nameof(Interpreten));
            }
        }

        private List<Record> records;

        public List<Record> Records
        {
            get { return records; }
            set
            {
                records = value;               
                RaisePropertyChangedEvent(nameof(Records));
            }
        }

        private Record selectedRecord;

        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                selectedRecord = value;
                Songs = db.Songs.Where(x => x.Record.RecordTitle == selectedRecord.RecordTitle).ToList();
                RaisePropertyChangedEvent(nameof(SelectedRecord));
            }
        }

        private List<Song> songs;

        public List<Song> Songs
        {
            get { return songs; }
            set
            {
                songs = value;
                RaisePropertyChangedEvent(nameof(Songs));
            }
        }

    }
}
