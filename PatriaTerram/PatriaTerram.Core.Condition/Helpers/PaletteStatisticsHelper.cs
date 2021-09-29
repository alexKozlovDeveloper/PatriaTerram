using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Condition.Helpers
{
    public static class PaletteStatisticsHelper
    {
        public static IEnumerable<string> GetAllTownNames(this PaletteStatistics paletteStatistic)
        {
            return paletteStatistic
                    .GetLayerValueRanges(Constants.BuildingLayer)
                    .Select(a => a.Key.Split('-').First())
                    .Distinct();
        }

        public static int GetMaxTerrainValue(this PaletteStatistics paletteStatistic)
        {
            return paletteStatistic
                .GetLayerValueRanges(Constants.TerrainLayer)
                .Select(a => a.Value.Top)
                .Max();
        }
    }
}
