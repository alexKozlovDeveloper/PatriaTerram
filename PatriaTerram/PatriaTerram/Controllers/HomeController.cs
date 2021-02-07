using PatriaTerram.Core.BuildingConditions;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
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

            return View(model);
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

            var conditions = new List<BuildingConditions>();

            for (int x = 0; x < points.Count; x++)
            {
                foreach (var item in points[x].BuildingConditions)
                {
                    conditions.Add(item);
                }
            }

            return conditions.Max(a => a.Value);
        }
    }
}