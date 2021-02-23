using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Game.Enums;

namespace PatriaTerram.Game.Entityes
{
    public class Step
    {
        public StepAction Action { get; set; }
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}
