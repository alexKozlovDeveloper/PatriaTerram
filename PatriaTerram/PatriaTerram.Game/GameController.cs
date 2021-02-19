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
using PatriaTerram.Core.Buildings;

namespace PatriaTerram.Game
{
    public class GameController
    {
        private Palette _map;
        private List<Step> _steps;
        private int stepNumber = 0;

        private BuildingBuilder _builder;

        public GameController(Palette map, List<Step> steps)
        {
            _map = map;
            _steps = steps;

            _builder = new BuildingBuilder(_map);
        }

        public void NextStep()
        {
            if(stepNumber >= _steps.Count) { return; }

            var step = _steps[stepNumber++];

            switch (step.Action)
            {
                case StepAction.Build:
                    Build(step.BuildingType, step.TownName);
                    break;
                default:
                    break;
            }
        }

        private void Build(BuildingType target, string townName)
        {
            BuildingConditionsProcessor.AddResultConditionLayer(_map, Configs.Buildings[target]);

            var coord = _map.GetMaxBuildingConditionCoordWithoutBuildings(target, TerrainType.Result.ToString());

            _builder.Build(target, townName, coord);
        }
    }
}
