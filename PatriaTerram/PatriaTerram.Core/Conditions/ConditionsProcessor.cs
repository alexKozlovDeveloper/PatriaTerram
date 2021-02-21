using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Models;
using System.Collections.Generic;

namespace PatriaTerram.Core.Conditions
{
    public class ConditionsProcessor
    {
        private Palette _palette;

        public ConditionsProcessor(Palette palette)
        {
            _palette = palette;
        }

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

        public void ResolveBuildingConditions(string townName, IEnumerable<Building> buildings)
        {
            var resolver = new ConditionsResolver(_palette);

            for (int x = 0; x < _palette.Width; x++)
            {
                for (int y = 0; y < _palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.ResolveBuildingCondition(new Coord(x, y), townName, building);
                    }
                }
            }
        }

        public void ResolveResultCondition(string townName, IEnumerable<Building> buildings)
        {
            foreach (var building in buildings)
            {
                ResolveResultCondition(townName, building);
            }
        }

        public void ResolveResultCondition(string townName, Building building)
        {
            var resolver = new ConditionsResolver(_palette);

            var maxConditions = resolver.GetMaxConditions(townName, building);

            foreach (var point in _palette.AllPoints)
            {
                resolver.ResolveResultCondition(point, townName, building, maxConditions);
            }
        }
    }
}
