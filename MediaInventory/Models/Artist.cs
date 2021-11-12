using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            MediaHasArtists = new HashSet<MediaHasArtist>();
        }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<MediaHasArtist> MediaHasArtists { get; set; }
    }
}
