using AStarAlgorithm.Entityes;
using PatriaTerram.Core;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Condition.Roads;
using PatriaTerram.Core.Conditions;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Game;
using PatriaTerram.Game.Entityes;
using PatriaTerram.Game.Enums;
using PatriaTerram.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PatriaTerram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Map");
        }

        public ActionResult Map()
        {
            Configs.PaletteConfigurationJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Files\PaletteConfigurations.json";
            Configs.TerrainsJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Files\Terrains.json";
            ConditionConfigs.TerrainsJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Files\Buildings.json";

            var factory = new TerrainPaletteFactory<ConditionPalettePoint>(Configs.PaletteConfigs["web"]);
            var model = factory.GetPalette();

            var stepFactory = new StepFactory();
            var steps = stepFactory.GetTwoKingdoms();

            var game = new GameController(model, steps, ConditionConfigs.Buildings.Values);

            while (game.IsHasSteps)
            {
                game.NextStep();
            }

            game.ResolveRoads(new List<BuildingType> { BuildingType.TownHall, BuildingType.Sawmill, BuildingType.Stonepit });

            //model.MovePointsXAxis(20);
            //model.MovePointsYAxis(20);

            var viewModel = new PaletteView(model);

            return View(viewModel);
        }

        public ActionResult LayersMenu(PaletteContext context)
        {
            var menu = new LayerMenuView(context);

            return View(menu);
        }

        public ActionResult MapPoint(PalettePointView model)
        {
            return View(model);
        }
    }
}