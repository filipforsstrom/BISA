using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public struct BusinessRulesDTO
    {
        public const int MaxLoansPerUser = 5;
        public const double BookLoanTime = 20;
        public const double MovieLoanTime = 7;
        public const double EbookLoanTime = 7;
    }
}
