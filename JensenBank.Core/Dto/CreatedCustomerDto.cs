using JensenBank.Core.Domain;
using Models.Domain;

namespace JensenBank.Core.Dto
{
    public class CreatedCustomerDto
    {
        public Customer? Customer_Details { get; set; }
        public User? User_Details { get; set; }
    }
}
