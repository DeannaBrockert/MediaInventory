using System;
using System.Collections.Generic;

#nullable disable

namespace MediaInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            MediaHasBorrowers = new HashSet<MediaHasBorrower>();
        }

        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<MediaHasBorrower> MediaHasBorrowers { get; set; }
    }
}
