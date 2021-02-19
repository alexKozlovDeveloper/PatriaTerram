﻿using AStarAlgorithm.Entityes;
using PatriaTerram.Core.BuildingConditions;
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
        private BuildingConditionsResolver _resolver;

        public BuildingBuilder(Palette palette)
        {
            _palette = palette;

            _resolver = new BuildingConditionsResolver(_palette);
        }

        public void Build(BuildingType target, Coord coord)
        {
            _palette[coord].Buildings.AddBuilding(target);

            _resolver.UpdateBuildingEffects(coord);
        }
    }
}