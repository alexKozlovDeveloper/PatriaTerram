using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Configurations;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Conditions.Resolvers;
using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Condition.Buildings
{
    public class BuildingBuilder
    {
        private Palette<ConditionPalettePoint> _palette;
        private PointBuildingConditionResolver _resolver;

        public BuildingBuilder(Palette<ConditionPalettePoint> palette)
        {
            _palette = palette;

            _resolver = new PointBuildingConditionResolver(_palette);
        }

        public void Build(BuildingType target, string townName, Coord coord)
        {
            _palette[coord].Buildings.AddIfNotExist(target, townName);

            _resolver.UpdateBuildingEffects(coord, ConditionConfigs.Buildings);
        }
    }
}
