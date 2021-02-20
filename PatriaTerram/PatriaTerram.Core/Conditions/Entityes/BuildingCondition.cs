using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Conditions.Entityes
{
    public class BuildingCondition : EnvironmentConditionBase
    {
        public BuildingType EnvironmentBuilding { get; set; }

        public override string ToString()
        {
            return $"{EnvironmentBuilding} " + base.ToString();
        }
    }
}
