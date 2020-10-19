
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Absa.Repo.DbContext.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactNumber = new HashSet<ContactNumber>();
        }

        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string FirstName { get; set; }
        [Required, MaxLength(255)]
        public string LastName { get; set; }
        public  ICollection<ContactNumber> ContactNumber { get; set; }
    }
}
