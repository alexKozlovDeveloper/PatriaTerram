using PatriaTerram.Core;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
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

            var factory = new TerrainPaletteFactory<ConditionPalettePoint>(Configs.PaletteConfigs["web"]);

            var model = factory.GetPalette();

            var stepFactory = new StepFactory();

            //var steps = GetSteps();
            var steps = new List<Step>();

            steps.AddRange(stepFactory.GetStartedPack("Farm_1"));
            steps.AddRange(stepFactory.GetStartedPack("Farm_2"));
            //steps.AddRange(stepFactory.GetStartedPack("Farm_3"));
            //steps.AddRange(stepFactory.GetStartedPack("Farm_4"));
            //steps.AddRange(stepFactory.GetStartedPack("Farm_5"));

            var game = new GameController(model, steps, ConditionConfigs.Buildings.Values);

            for (int i = 0; i < 1000; i++)
            {
                if(i == 27)
                {

                }

                game.NextStep();
            }

            model.MovePointsXAxis(20);
            model.MovePointsYAxis(20);

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

        private List<Step> GetSteps()
        {
            var townName = "FarmVillage";

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

            var steps = new List<Step>
            {
                townHall
            };

            steps.AddRange(startedPack);
            steps.AddRange(startedPack);
            steps.AddRange(startedPack);
            steps.AddRange(startedPack);

            return steps;
        }
    }
}