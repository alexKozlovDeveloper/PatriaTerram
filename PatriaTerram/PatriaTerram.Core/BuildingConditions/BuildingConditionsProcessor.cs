using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.BuildingConditions
{
    public class BuildingConditionsProcessor
    {
        public void Resolve(Palette palette)
        {
            IEnumerable<IConditionsResolver> resolvers = new List<IConditionsResolver> 
            { 
                new TownHallConditionResolver() 
            };

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    foreach (var resolver in resolvers)
                    {
                        resolver.Resolve(palette, new Coord(x, y));
                    }
                }
            }
        }
    }
}
