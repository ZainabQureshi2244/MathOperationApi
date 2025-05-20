using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp_Repo.DTOs
{
    public class CreateMathOperationDto
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public string OperationType { get; set; }
    }
}
