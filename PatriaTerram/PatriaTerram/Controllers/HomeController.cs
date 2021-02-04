using PatriaTerram.Core.Factoryes;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatriaTerram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IPaletteFactory factory = new TerrainPaletteFactory();
            //IPaletteFactory factory = new SamplePaletteFactory();

            var model = factory.GetPalette();

            return View(model);
        }        
    }
}