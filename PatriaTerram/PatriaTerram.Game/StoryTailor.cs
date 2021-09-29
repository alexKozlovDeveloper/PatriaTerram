using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Factoryes;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Logging;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Game
{
    public class StoryTailor
    {
        private ILogger _log;

        public StoryTailor(ILogger log)
        {
            _log = log;
        }

        public Palette<ConditionPalettePoint> Tell()
        {
            _log.Log($"Starting...");
            var factory = new TerrainPaletteFactory<ConditionPalettePoint>(
                                Configs.PaletteConfigs["web_small"], 
                                new ConditionPalettePointFactory()
                                );

            _log.Log($"Creating Palette...");
            var model = factory.GetPalette();

            _log.Log($"Getting Steps...");
            var stepFactory = new StepFactory();

            //var steps = stepFactory.GetBigCounty();
            //var steps = stepFactory.GetTwoKingdoms();
            var steps = stepFactory.GetFarmVillage("Farm_1");

            var game = new GameController(model, steps, ConditionConfigs.Buildings.Values);

            _log.Log($"Resolving Steps...");

            for (int i = 1; game.IsHasSteps; i++)
            {
                _log.Log($"Resolving Step [{i} / {game.StepsCount}]...");
                game.NextStep();
            }

            _log.Log($"Resolving Roads...");

            foreach (var town in model.GetAllTownNames())
            {
                _log.Log($"Resolving Roads for town {town}...");
                game.ResolveRoads(town);
            }

            _log.Log($"Resolving Global Roads...");
            game.ResolveRoads(new List<BuildingType> { BuildingType.TownHall, BuildingType.FarmTownHall, BuildingType.StonepitTownHall });

            _log.Log($"Finishing...");

            // TODO: remove
            var d = model.Statistics.GetReport();

            return model;
        }
    }
}
