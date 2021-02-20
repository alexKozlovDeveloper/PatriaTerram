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
        private Palette _palette;

        public ConditionsProcessor(Palette palette)
        {
            _palette = palette;
        }

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

        public void ResolveTerrainConditions(IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(_palette);

            for (int x = 0; x < _palette.Width; x++)
            {
                for (int y = 0; y < _palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolveTerrainCondition(new Coord(x, y), building);
                    }
                }
            }
        }

        public void ResolveBuildingConditions(IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(_palette);

            for (int x = 0; x < _palette.Width; x++)
            {
                for (int y = 0; y < _palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolveBuildingCondition(new Coord(x, y), building);
                    }
                }
            }
        }

        public void ResolveResultCondition(IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(_palette);

            foreach (var building in buildings)
            {
                var maxConditions = resolver.GetMaxConditions(building);

                foreach (var point in _palette.AllPoints)
                {
                    resolver.ResolveResultCondition(point, building, maxConditions);
                }
            }
        }

        public void ResolveResultCondition(Building building)
        {
            var resolver = new ConditionsResolver(_palette);

            var maxConditions = resolver.GetMaxConditions(building);

            foreach (var point in _palette.AllPoints)
            {
                resolver.ResolveResultCondition(point, building, maxConditions);
            }
        }

        public static void AddResultConditionLayer(Palette palette, Building building)
        {
            //var resolver = new ConditionsResolver(palette);

            //resolver.FinalResolve(building);

            var res = new ConditionsProcessor(palette);

            res.ResolveResultCondition(building);
            //resolver.ResolveResultCondition(building);
        }
    }
}
