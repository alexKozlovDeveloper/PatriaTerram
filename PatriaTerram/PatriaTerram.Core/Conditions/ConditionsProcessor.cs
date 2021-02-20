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
            foreach (var building in buildings)
            {
                ResolveResultCondition(building);
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
    }
}
