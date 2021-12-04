using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required (ErrorMessage="Please enter a first name.")]
        public string FirstName { get; set; }
        [Required (ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Please enter a phone number.")]
        public string PhoneNumber { get; set; }
        

        public virtual ICollection<MediaHasBorrower> MediaHasBorrowers { get; set; }
    }
}
