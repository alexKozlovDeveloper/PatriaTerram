using PatriaTerram.Core.Models;
using System.Collections.Generic;
using System.Linq;
using PatriaTerram.Game.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Conditions;
using PatriaTerram.Game.Enums;
using PatriaTerram.Core.Condition.Buildings;
using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Roads;
using AStarAlgorithm.Entityes;

namespace PatriaTerram.Game
{
    public class GameController
    {
        private Palette<ConditionPalettePoint> _map;
        private List<Step> _steps;
        private int stepNumber = 0;

        private BuildingBuilder _builder;
        private ConditionsProcessor _conditionsProcessor;
        private IEnumerable<Building> _buildings;

        private List<string> _townNames;

        public GameController(Palette<ConditionPalettePoint> map, List<Step> steps, IEnumerable<Building> buildings)
        {
            _map = map;
            _steps = steps;

            _townNames = _steps.Select(a => a.TownName)
                               .Distinct()
                               .ToList();

            _builder = new BuildingBuilder(_map);

            _buildings = buildings;

            _conditionsProcessor = new ConditionsProcessor(_map);

            _conditionsProcessor.ResolveTerrainConditions(_buildings);
        }

        public bool IsHasSteps
        {
            get
            {
                return stepNumber < _steps.Count;
            }
        }

        public int StepsCount
        {
            get
            {
                return _steps.Count;
            }
        }

        public void NextStep()
        {
            if(stepNumber >= _steps.Count) { return; }

            var step = _steps[stepNumber++];

            switch (step.Action)
            {
                case StepAction.Build:
                    Build(step.TownName, step.BuildingType);
                    break;
                default:
                    break;
            }
        }

        private void Build(string townName, BuildingType buildingType)
        {
            var building = _buildings.FirstOrDefault(a => a.Type == buildingType);

            _conditionsProcessor.ResolveResultCondition(townName, _townNames, building);

            var coord = _map.GetMaxBuildingConditionCoordWithoutBuildings(townName, buildingType, TerrainType.Result.ToString());

            _builder.Build(buildingType, townName, coord);
        }

        public void ResolveRoads(IEnumerable<BuildingType> buildingTypes)
        {
            var roadBuilder = new RoadBuilder(_map);

            var buildingCoords = new List<Coord>();

            foreach (var buildingType in buildingTypes)
            {
                buildingCoords.AddRange(_map.GetAllBuildingCoords(buildingType));
            }

            for (int i = 0; i < buildingCoords.Count - 1; i++)
            {
                for (int j = i + 1; j < buildingCoords.Count; j++)
                {
                    var start = buildingCoords[i];
                    var finish = buildingCoords[j];

                    roadBuilder.Build(start, finish, "World_Roads");
                }
            }
        }
    }
}
