﻿using AStarAlgorithm.Entityes;
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

        public void ResolveResultCondition(string townName, List<string> allTownNames, IEnumerable<Building> buildings)
        {
            foreach (var building in buildings)
            {
                ResolveResultCondition(townName, allTownNames, building);
            }
        }

        public void ResolveResultCondition(string townName, List<string> allTownNames, Building building)
        {
            var resolver = new ConditionsResolver(_palette);

            var maxConditions = resolver.GetMaxConditions(allTownNames, building);
            var items = resolver.GetConditionRanges(allTownNames, building);

            foreach (var point in _palette.AllPoints)
            {
                resolver.ResolveResultCondition(point, townName, building, items);
            }
        }
    }
}
