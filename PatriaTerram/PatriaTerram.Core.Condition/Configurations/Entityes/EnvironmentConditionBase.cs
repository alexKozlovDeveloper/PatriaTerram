using PatriaTerram.Core.Condition.Enums;

namespace PatriaTerram.Core.Condition.Configurations.Entityes
{
    public class EnvironmentConditionBase
    {
        public int Radius { get; set; }
        public int InnerRadius { get; set; }
        public int Priority { get; set; }
        public EnvironmentConditionType Type { get; set; }
        public bool IsPositive { get; set; }
        public bool IsRequired { get; set; }

        public override string ToString()
        {
            var posValue = IsPositive == true ? "positive" : "negative";
            var req = IsRequired == true ? "<!>" : "";
            return $"{req} [R:{Radius},P:{Priority},T:{Type}] {posValue}";
        }
    }
}
