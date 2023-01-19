using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JensenBank.Core.Dto
{
    public class DbUserForCreationDto
    {
        public required int CustomerId { get; set; }
        public required string Username { get; set; }
        public required string PW_Hash { get; set; }
        public required string PW_Salt { get; set; }
        public required int User_RoleId { get; set; }
    }
}
