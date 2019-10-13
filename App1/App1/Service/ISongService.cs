using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Entity;

namespace App1.Service
{
    interface ISongService
    {
        Song PostSongFree(Song song);

        ObservableCollection<Song> GetFreeSongs();
    }
}
