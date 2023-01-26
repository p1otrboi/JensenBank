using System.ComponentModel.DataAnnotations;

namespace JensenBank.Core.Dto
{
    public class MoneyTransferDto
    {
        public int From_Account { get; set; }
        public int To_Account { get; set; }
        [Range (0, int.MaxValue)]
        public decimal Amount { get; set; }
    }
}
