using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Web.Models
{
    public class MapCellItem
    {
        public Color Color { get; set; }
        public int Value { get; set; }
        public List<string> Classes { get; set; }

        public MapCellItem()
        {
            Classes = new List<string>();
        }
    }
}