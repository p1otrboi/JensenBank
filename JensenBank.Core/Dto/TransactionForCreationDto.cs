using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Core.Dto
{
    public class TransactionForCreationDto
    {
        public required int AccountId { get; set; }
        public required string Type { get; set; }
        public required string Operation { get; set; }
        public required decimal Amount { get; set; }
        public required decimal Balance { get; set;}
    }
}
