using PatriaTerram.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Game.Entityes
{
    public class Step
    {
        public StepAction Action { get; set; }
        public string TownName { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}
