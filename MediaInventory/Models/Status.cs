using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class Status
    {
        public Status()
        {
            Media = new HashSet<Medium>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Medium> Media { get; set; }
    }
}
