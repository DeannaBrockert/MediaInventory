using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class Medium
    {
        public Medium()
        {
            MediaHasArtists = new HashSet<MediaHasArtist>();
            MediaHasBorrowers = new HashSet<MediaHasBorrower>();
        }

        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MediaTypeId { get; set; }
        public int StatusId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<MediaHasArtist> MediaHasArtists { get; set; }
        public virtual ICollection<MediaHasBorrower> MediaHasBorrowers { get; set; }
    }
}
