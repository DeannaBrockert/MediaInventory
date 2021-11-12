using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class MediaHasArtist
    {
        public int MediaHasArtist1 { get; set; }
        public int ArtistId { get; set; }
        public int MediaId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Medium Media { get; set; }
    }
}
