using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class MediaHasBorrower
    {
        public int MediaHasBorrowerId { get; set; }
        public int BorrowerId { get; set; }
        public int MediaId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Medium Media { get; set; }
    }
}
