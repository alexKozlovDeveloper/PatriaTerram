﻿using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models.Layers.Entityes
{
    public class BuildingLayerItem : ILayerItem
    {
        public BuildingType BuildingType { get; set; }
    }
}