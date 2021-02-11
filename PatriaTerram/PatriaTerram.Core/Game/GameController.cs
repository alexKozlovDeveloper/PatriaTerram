using PatriaTerram.Core.Models;
using PatriaTerram.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.BuildingConditions;

namespace PatriaTerram.Core.Game
{
    public class GameController
    {
        private Palette _map;
        private List<Step> _steps;
        private int stepNumber = 0;

        public GameController(Palette map)
        {
            _map = map;

            var townHall = new Step
            {
                Action = "Build",
                Target = Constants.TownHall
            };
            var sawmill = new Step
            {
                Action = "Build",
                Target = Constants.Sawmill
            };
            var farm = new Step
            {
                Action = "Build",
                Target = Constants.Farm
            };
            var house = new Step
            {
                Action = "Build",
                Target = Constants.House
            };
            var stonepit = new Step
            {
                Action = "Build",
                Target = Constants.Stonepit
            };

            var startedPack = new List<Step>() 
            {
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

            _steps = new List<Step> 
            {
                townHall
            };

            _steps.AddRange(startedPack);
            _steps.AddRange(startedPack);
            _steps.AddRange(startedPack);
            _steps.AddRange(startedPack);
        }

        public void NextStep()
        {
            if(stepNumber >= _steps.Count) { return; }

            var step = _steps[stepNumber++];

            switch (step.Action)
            {
                case "Build":
                    Build(step.Target);
                    break;
                default:
                    break;
            }
        }

        private void Build(string target)
        {
            BuildingConditionsProcessor.AddResultConditionLayer(_map, Configs.Buildings[target]);

            var coord = _map.GetMaxBuildingConditionCoordWithoutBuildings(target, "result");

            var building = Configs.Buildings[target];

            _map[coord].Buildings.Add(building.Name, building);

            BuildingConditionsResolver.UpdateBuildingEffects(_map, coord);
        }
    }
}
