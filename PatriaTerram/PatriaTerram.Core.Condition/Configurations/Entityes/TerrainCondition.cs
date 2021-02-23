using PatriaTerram.Core.Enums;

namespace PatriaTerram.Core.Condition.Configurations.Entityes
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
