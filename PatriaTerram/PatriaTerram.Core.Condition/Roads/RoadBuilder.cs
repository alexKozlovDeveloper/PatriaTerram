using AStarAlgorithm;
using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Buildings;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Condition.Roads
{
    public class RoadBuilder
    {
        private Palette<ConditionPalettePoint> _palette;

        public RoadBuilder(Palette<ConditionPalettePoint> palette)
        {
            _palette = palette;
        }

        public void Build(Coord start, Coord finish, string townName)
        {
            var converter = new PassabilityMatrixConverter(ConditionConfigs.TerrainPassability, ConditionConfigs.BuildingPassability);

            var matrix = converter.Convert(_palette);

            var aStartResolver = new AStarResolver(matrix);

            var path = aStartResolver.GetPath(start, finish);

            var builder = new BuildingBuilder(_palette);

            foreach (var item in path)
            {
                builder.Build(BuildingType.Road, townName, item);
            }
        }
    }
}
