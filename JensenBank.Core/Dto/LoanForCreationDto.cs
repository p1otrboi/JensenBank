using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Core.Dto
{
    public class LoanForCreationDto
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public required int Duration_Months { get; set; }
    }
}
