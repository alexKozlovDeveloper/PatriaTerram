using PatriaTerram.Core.Models;
using PatriaTerram.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Game.Entityes;
using PatriaTerram.Core;
using PatriaTerram.Core.Enums;

namespace PatriaTerram.Game
{
    public class GameController
    {
        private Palette _map;
        private List<Step> _steps;
        private int stepNumber = 0;

        public GameController(Palette map, List<Step> steps)
        {
            _map = map;
            _steps = steps; 
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

            var coord = _map.GetMaxBuildingConditionCoordWithoutBuildings(target, TerrainType.Result.ToString());

            var building = Configs.Buildings[target];

            _map[coord].Buildings.Add(building.Name, building);

            BuildingConditionsResolver.UpdateBuildingEffects(_map, coord);
        }
    }
}
