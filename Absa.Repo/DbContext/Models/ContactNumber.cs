
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absa.Repo.DbContext.Models
{
    public class ContactNumber
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ContactId { get; set; }
        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }
        [ForeignKey("FK_Contact_ContactNumber")]
        public Contact Contact { get; set; }
    }
}
