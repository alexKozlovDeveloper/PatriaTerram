using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class EnvironmentCondition
    {
        public string Environment { get; set; }
        public int Radius { get; set; }
        public int Priority { get; set; }
        public EnvironmentConditionType Type { get; set; }
        public bool IsPositive { get; set; }

        public override string ToString()
        {
            var posValue = IsPositive == true ? "positive" : "negative";
            return $"{Environment}-[R:{Radius},P:{Priority},T:{Type}] {posValue}";
        }
    }
}
