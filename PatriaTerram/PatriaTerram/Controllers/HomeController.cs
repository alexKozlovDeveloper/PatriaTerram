using PatriaTerram.Core;
using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
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
            Configs.PaletteConfigurationJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\PaletteConfigurations.json";
            Configs.TerrainsJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Terrains.json";
            
            IPaletteFactory factory = new TerrainPaletteFactory(Configs.PaletteConfigs["web"]);

            var model = factory.GetPalette();

            var processor = new BuildingConditionsProcessor();

            processor.Resolve(model);

            ViewBag.maxCondition = GetMaxCondition(model);

            // test cityHoll

            double maxWood = model.GetMaxBuildingConditionValue(Constants.TownHall, Constants.Wood);
            double maxFertileSoil = model.GetMaxBuildingConditionValue(Constants.TownHall, Constants.FertileSoil);
            double maxLake = model.GetMaxBuildingConditionValue(Constants.TownHall, Constants.Lake);
            double maxStone = model.GetMaxBuildingConditionValue(Constants.TownHall, Constants.Stone);

            ViewBag.maxConditions = new Dictionary<string, int>();

            ViewBag.maxConditions.Add(Constants.Wood, (int)maxWood);
            ViewBag.maxConditions.Add(Constants.FertileSoil, (int)maxFertileSoil);
            ViewBag.maxConditions.Add(Constants.Lake, (int)maxLake);
            ViewBag.maxConditions.Add(Constants.Stone, (int)maxStone);
            ViewBag.maxConditions.Add("result", 255);
            //ViewBag.maxConditions.Add(Constants., maxStone);

            var res = new int[model.Width][];

            for (int i = 0; i < model.Height; i++)
            {
                res[i] = new int[model.Height];
            }

            var maxValue = 0;
            var maxX = 0;
            var maxY = 0;

            for (int x = 0; x < model.Width; x++)
            {
                for (int y = 0; y < model.Height; y++)
                {
                    var conditions = model[x, y].BuildingConditions[Constants.TownHall];

                    var wood = GetValue(conditions, Constants.Wood, maxWood);
                    var stone = GetValue(conditions, Constants.Stone, maxStone);
                    var lake = GetValue(conditions, Constants.Lake, maxLake);
                    var fertileSoil = GetValue(conditions, Constants.FertileSoil, maxFertileSoil);

                    int value = wood + stone + lake + fertileSoil;

                    res[x][y] = value;

                    if(value > maxValue)
                    {
                        maxValue = value;
                        maxX = x;
                        maxY = y;
                    }
                }
            }



            return View(model);
        }

        public ActionResult Map()
        {
            Configs.PaletteConfigurationJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\PaletteConfigurations.json";
            Configs.TerrainsJsonFilePath = @"C:\Users\alexk\OneDrive\Документы\GitHub\PatriaTerram\PatriaTerram\PatriaTerram\bin\Configurations\Terrains.json";

            IPaletteFactory factory = new TerrainPaletteFactory(Configs.PaletteConfigs["web"]);

            var model = factory.GetPalette();

            var processor = new BuildingConditionsProcessor();

            processor.Resolve(model);

            var viewModel = new PaletteView(model);

            return View(viewModel);
        }
        
        public ActionResult MapPoint(PalettePointView model)
        {          
            return View(model);
        }

        public ActionResult PalettePoint()
        {
            var point = new PalettePoint();

            return View(point);
        }

        private int GetValue(BuildingCondition conditions, string terrain, double max)
        {
            if(conditions.TerrainConditionValues.Keys.Contains(terrain) == false)
            {
                return 0;
            }

            return (int)((conditions.TerrainConditionValues[terrain] / max) * 2500);
        }

        private int GetMaxCondition(Palette model)
        {
            var points = new List<PalettePoint>();

            for (int x = 0; x < model.Width; x++)
            {
                for (int y = 0; y < model.Height; y++)
                {
                    points.Add(model[x, y]);
                }
            }

            var conditions = new List<BuildingCondition>();

            for (int x = 0; x < points.Count; x++)
            {
                foreach (var item in points[x].BuildingConditions)
                {
                    conditions.Add(item.Value);
                }
            }

            return conditions.Max(a => a.TerrainConditionValues.Values.Max());
        }
    }
}