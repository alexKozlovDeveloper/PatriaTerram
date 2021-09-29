using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Conditions.Resolvers;
using PatriaTerram.Core.Models;
using System.Collections.Generic;

namespace PatriaTerram.Core.Conditions
{
    public class ConditionsProcessor
    {
        private Palette<ConditionPalettePoint> _palette;

        public ConditionsProcessor(Palette<ConditionPalettePoint> palette)
        {
            _palette = palette;
        }

        public void ResolveTerrainConditions(IEnumerable<Building> buildings)
        {
            var resolver = new PointTerrainConditionResolver(_palette);

            for (int x = 0; x < _palette.Width; x++)
            {
                for (int y = 0; y < _palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.Resolve(new Coord(x, y), building);
                    }
                }
            }
        }

        public void ResolveBuildingConditions(string townName, IEnumerable<Building> buildings)
        {
            var resolver = new PointBuildingConditionResolver(_palette);

            for (int x = 0; x < _palette.Width; x++)
            {
                for (int y = 0; y < _palette.Height; y++)
                {
                    foreach (var building in buildings)
                    {
                        resolver.Resolve(new Coord(x, y), townName, building);
                    }
                }
            }
        }

        public void ResolveResultCondition(string townName, IEnumerable<Building> buildings)
        {
            var allTownNames = _palette.GetAllTownNames();

            foreach (var building in buildings)
            {
                ResolveResultCondition(townName, allTownNames, building);
            }
        }

        public void ResolveResultCondition(string townName, List<string> allTownNames, Building building)
        {
            var resolver = new PointResultConditionResolver(_palette);

            var items = _palette.GetConditionRanges(allTownNames, building);

            var d = _palette.Statistics.DescriptorValueRanges;

            foreach (var point in _palette.AllPoints)
            {
                resolver.Resolve(point, townName, building, items);
            }
        }
    }
}
