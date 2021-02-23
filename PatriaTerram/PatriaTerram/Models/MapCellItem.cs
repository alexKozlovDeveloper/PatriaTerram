using PatriaTerram.Core.Configurations.Entityes;
using System.Collections.Generic;

namespace PatriaTerram.Web.Models
{
    public class MapCellItem
    {
        public Color Color { get; set; }
        public int Value { get; set; }
        public string Image { get; set; }
        public List<string> Classes { get; set; }

        public MapCellItem()
        {
            Classes = new List<string>();
        }
    }
}