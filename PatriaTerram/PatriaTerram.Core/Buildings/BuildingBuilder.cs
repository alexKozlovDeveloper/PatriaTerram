using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Conditions;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Buildings
{
    public class BuildingBuilder
    {
        private Palette _palette;
        private ConditionsResolver _resolver;

        public BuildingBuilder(Palette palette)
        {
            _palette = palette;

            _resolver = new ConditionsResolver(_palette);
        }

        public void Build(BuildingType target, string townName, Coord coord)
        {
            _palette[coord].Buildings.AddBuilding(target, townName);

            _resolver.UpdateBuildingEffects(coord);
        }
    }
}
