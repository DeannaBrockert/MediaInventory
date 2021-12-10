using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required (ErrorMessage = "Please enter a media title.")]
        public string MediaName { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required (ErrorMessage = "Please select a media type.")]
        public int MediaTypeId { get; set; }
        [Required (ErrorMessage = "Please select a status.")]
        public int StatusId { get; set; }
        [Required (ErrorMessage = "Please select a genre.")]
        public int GenreId { get; set; }


        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<MediaHasArtist> MediaHasArtists { get; set; }
        public virtual ICollection<MediaHasBorrower> MediaHasBorrowers { get; set; }
    }
}
