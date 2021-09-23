using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAssessment.Models.Entities
{
    public class CollateralRisk
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int RiskPercent { get; set; }
        public DateTime DateAssessed { get; set; }
    }
}
