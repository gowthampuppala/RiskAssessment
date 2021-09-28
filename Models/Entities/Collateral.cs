using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAssessment.Models.Entities
{
    public partial class Collateral
    {
		public int Id { get; set; }
		public int LoanId { get; set; }
		public int CustomerId { get; set; }
		public string Type { get; set; }
		public int unitValue { get; set; }
		public int NoOfUnits { get; set; }
		public DateTime InitialAssesDate { get; set; }
		
	}
}
