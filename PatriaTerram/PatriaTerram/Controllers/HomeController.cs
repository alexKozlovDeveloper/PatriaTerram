using PatriaTerram.Core;
using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Game;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
using PatriaTerram.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

            var game = new GameController(model);
            game.NextStep();
            //game.NextStep();
            //game.NextStep();
            //game.NextStep();
            //// test

            //var coords = model.GetMaxBuildingConditionCoords(Constants.TownHall, "result");

            //var sortedKeys = coords.Keys.ToList();
            //sortedKeys.Sort();
            //sortedKeys.Reverse();

            ////var mostCoors = new List<Coord>();

            //for (int i = 0; i < 10; i++)
            //{
            //    //mostCoors.AddRange(coords[sortedKeys[i]]);

            //    foreach (var coord in coords[sortedKeys[i]])
            //    {
            //        model[coord].Buildings.Add(Constants.TownHall, new Building()
            //        {
            //            Color = new Color()
            //            {
            //                R = 255,
            //                G = 20,
            //                B = 147
            //            },
            //            Name = Constants.TownHall,
            //            Value = sortedKeys[i]
            //        });
            //    }
            //}



            //// end test

            var viewModel = new PaletteView(model);

            return View(viewModel);
        }
        
        public ActionResult MapPoint(PalettePointView model)
        {          
            return View(model);
        }
    }
}