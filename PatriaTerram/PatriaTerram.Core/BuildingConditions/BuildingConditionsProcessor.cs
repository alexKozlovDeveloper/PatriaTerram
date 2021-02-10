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
        //public void Resolve(Palette palette)
        //{
        //    IEnumerable<IConditionsResolver> resolvers = new List<IConditionsResolver>
        //    {
        //        new TownHallConditionResolver()
        //    };

        //    for (int x = 0; x < palette.Width; x++)
        //    {
        //        for (int y = 0; y < palette.Height; y++)
        //        {
        //            foreach (var resolver in resolvers)
        //            {
        //                resolver.ResolvePoint(palette, new Coord(x, y));
        //            }
        //        }
        //    }

        //    foreach (var resolver in resolvers)
        //    {
        //        resolver.FinalResolve(palette);
        //    }
        //}

        public void Resolve(Palette palette, IEnumerable<Building> buildings)
        {
            var resolver = new BuildingConditionsResolver();

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolvePoint(palette, new Coord(x, y), building);
                    }
                }
            }

            foreach (var building in buildings)
            {
                resolver.FinalResolve(palette, building);
            }            
        }
    }
}
