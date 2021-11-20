using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string ArtistName { get; set; }
        [Required]
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<MediaHasArtist> MediaHasArtists { get; set; }
    }
}
