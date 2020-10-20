
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absa.Repo.DbContext.Models
{
    public class ContactNumber
    {
        [Key]
        public int Id { get; set; }
        [Required, ForeignKey("FK_Contact_ContactNumber")]
        public int ContactId { get; set; }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }
        public Contact Contact { get; set; }
    }
}
