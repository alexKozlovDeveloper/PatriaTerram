using PatriaTerram.Core.Models;
using PatriaTerram.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Game.Entityes;
using PatriaTerram.Core;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Buildings;
using PatriaTerram.Core.Conditions;

namespace PatriaTerram.Game
{
    public class GameController
    {
        private Palette _map;
        private List<Step> _steps;
        private int stepNumber = 0;

        private BuildingBuilder _builder;
        private ConditionsProcessor _conditionsProcessor;
        private IEnumerable<Building> _buildings;

        public GameController(Palette map, List<Step> steps, IEnumerable<Building> buildings)
        {
            _map = map;
            _steps = steps;

            _builder = new BuildingBuilder(_map);

            _buildings = buildings;

            _conditionsProcessor = new ConditionsProcessor(_map);

            _conditionsProcessor.ResolveTerrainConditions(_buildings);
            _conditionsProcessor.ResolveResultCondition(_buildings);
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

        private void Build(BuildingType buildingType, string townName)
        {
            var building = _buildings.FirstOrDefault(a => a.Type == buildingType);

            _conditionsProcessor.ResolveResultCondition(building);

            var coord = _map.GetMaxBuildingConditionCoordWithoutBuildings(buildingType, TerrainType.Result.ToString());

            _builder.Build(buildingType, townName, coord);
        }
    }
}
