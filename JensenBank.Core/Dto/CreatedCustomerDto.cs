using JensenBank.Core.Domain;
using Models.Domain;

namespace JensenBank.Core.Dto
{
    public class CreatedCustomerDto
    {
        public Customer? Customer_Details { get; set; }
        public Account? Account { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
