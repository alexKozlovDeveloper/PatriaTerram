using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Conditions.Entityes
{
    public class TerrainCondition : EnvironmentConditionBase
    {
        public TerrainType EnvironmentTerrain { get; set; }

        public override string ToString()
        {
            return $"{EnvironmentTerrain} " + base.ToString();
        }
    }
}
