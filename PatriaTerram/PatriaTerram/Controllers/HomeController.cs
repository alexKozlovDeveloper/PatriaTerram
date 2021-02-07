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
            IPaletteFactory factory = new TerrainPaletteFactory(Configs.PaletteConfigs["main"]);
            //IPaletteFactory factory = new SamplePaletteFactory();

            var model = factory.GetPalette();

            // test conditions

            var processor = new BuildingConditionsProcessor();

            processor.Resolve(model);

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

            //var points = model.Points.Cast<PalettePoint>().ToList();
            //points.Select(a => a.BuildingConditions).Cast<BuildingConditions>().ToList();

            var max = conditions.Max(a => a.Value);

            ViewBag.maxCondition = max;



            return View(model);
        }        
    }
}