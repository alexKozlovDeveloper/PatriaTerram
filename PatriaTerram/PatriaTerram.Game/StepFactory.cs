using PatriaTerram.Core.Enums;
using PatriaTerram.Game.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatriaTerram.Game.Enums;

namespace PatriaTerram.Game
{
    public class StepFactory
    {
        public List<Step> GetStartedPack(string townName)
        {
            var townHall = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.TownHall,
                TownName = townName
            };
            var sawmill = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Sawmill,
                TownName = townName
            };
            var farm = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Farm,
                TownName = townName
            };
            var house = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.House,
                TownName = townName
            };
            var stonepit = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Stonepit,
                TownName = townName
            };

            var startedPack = new List<Step>()
            {
                townHall,
                sawmill,
                sawmill,
                sawmill,
                stonepit,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                house,
                house,
                house,
                house,
                house,
                house,
                house,
            };

            return startedPack;
        }
    }
}
