
using System.Collections.Generic;

namespace Absa.API.DtoModels
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ContactDetail> ContactDetails { get; set; }
    }
}
