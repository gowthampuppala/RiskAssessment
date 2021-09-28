using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAssessment.Models.Entities
{
    public class CurrentValue
    {
        public int Id { get; set; }
        public int GoldUnitValue { get; set; }
        public int LandUnitValue
        {
            get; set;
        }
        public int OtherAssetUnitValue {get; set;}
    }
}
