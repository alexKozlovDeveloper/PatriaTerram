using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Conditions
{
    public class ConditionsProcessor
    {
        //public void Resolve(Palette palette, IEnumerable<Building> buildings)
        //{
        //    var resolver = new ConditionsResolver(palette);

        //    for (int x = 0; x < palette.Width; x++)
        //    {
        //        for (int y = 0; y < palette.Height; y++)
        //        {
        //            foreach (var building in buildings)
        //            {
        //                resolver.ResolveTerrainCondition(new Coord(x, y), building);
        //            }
        //        }
        //    }

        //    foreach (var building in buildings)
        //    {
        //        resolver.FinalResolve(building);
        //    }            
        //}

        public void ResolveTerrainConditions(Palette palette, IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(palette);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolveTerrainCondition(new Coord(x, y), building);
                    }
                }
            }
        }

        public void ResolveBuildingConditions(Palette palette, IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(palette);

            for (int x = 0; x < palette.Width; x++)
            {
                for (int y = 0; y < palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolveBuildingCondition(new Coord(x, y), building);
                    }
                }
            }
        }

        public void ResolveResultCondition(Palette palette, IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(palette);

            foreach (var building in buildings)
            {
                var maxConditions = resolver.GetMaxConditions(building);

                foreach (var point in palette.AllPoints)
                {
                    resolver.ResolveResultCondition(point, building, maxConditions);
                }
            }
        }

        public void ResolveResultCondition(Palette palette, Building building)
        {
            var resolver = new ConditionsResolver(palette);

            var maxConditions = resolver.GetMaxConditions(building);

            foreach (var point in palette.AllPoints)
            {
                resolver.ResolveResultCondition(point, building, maxConditions);
            }
        }

        public static void AddResultConditionLayer(Palette palette, Building building)
        {
            //var resolver = new ConditionsResolver(palette);

            //resolver.FinalResolve(building);

            var res = new ConditionsProcessor();

            res.ResolveResultCondition(palette, building);
            //resolver.ResolveResultCondition(building);
        }
    }
}
