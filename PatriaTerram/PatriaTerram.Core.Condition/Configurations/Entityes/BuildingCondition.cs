using PatriaTerram.Core.Condition.Enums;

namespace PatriaTerram.Core.Condition.Configurations.Entityes
{
    public class BuildingCondition : EnvironmentConditionBase
    {
        public BuildingType EnvironmentBuilding { get; set; }
        public TownCondition TownCondition { get; set; }

        public override string ToString()
        {
            return $"{EnvironmentBuilding} " + base.ToString();
        }
    }
}
