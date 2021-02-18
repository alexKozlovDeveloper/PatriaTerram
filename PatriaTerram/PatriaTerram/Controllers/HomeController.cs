using PatriaTerram.Core;
using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Game;
using PatriaTerram.Game.Entityes;
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
            Configs.PaletteConfigurationJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\PaletteConfigurations.json";
            Configs.TerrainsJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Terrains.json";

            IPaletteFactory factory = new TerrainPaletteFactory(Configs.PaletteConfigs["web"]);

            var model = factory.GetPalette();

            var processor = new BuildingConditionsProcessor();

            processor.Resolve(model, Configs.Buildings.Values);

            var steps = GetSteps();

            var game = new GameController(model, steps);

            for (int i = 0; i < 1000; i++)
            {
                game.NextStep();
            }

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
            var townHall = new Step
            {
                Action = "Build",
                BuildingType = BuildingType.TownHall
            };
            var sawmill = new Step
            {
                Action = "Build",
                BuildingType = BuildingType.Sawmill
            };
            var farm = new Step
            {
                Action = "Build",
                BuildingType = BuildingType.Farm
            };
            var house = new Step
            {
                Action = "Build",
                BuildingType = BuildingType.House
            };
            var stonepit = new Step
            {
                Action = "Build",
                BuildingType = BuildingType.Stonepit
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