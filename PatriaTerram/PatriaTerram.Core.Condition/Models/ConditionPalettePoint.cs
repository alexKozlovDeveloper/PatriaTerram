using PatriaTerram.Core.Condition.Layers;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Layers;
using PatriaTerram.Core.Models;
using System.Collections.Generic;

namespace PatriaTerram.Core.Condition.Models
{
    public class ConditionPalettePoint : TerrainPalettePoint
    {
        public ConditionPalettePoint(int x, int y) : base(x, y)
        {
            _layers.Add(new TerrainConditionLayer());
            _layers.Add(new BuildingConditionLayer());
            _layers.Add(new ResultConditionLayer());
            _layers.Add(new BuildingLayer());
        }

        public TerrainConditionLayer TerrainConditions => GetLayer<TerrainConditionLayer>("TerrainCondition");
        public BuildingConditionLayer BuildingConditions => GetLayer<BuildingConditionLayer>("BuildingCondition");        
        public ResultConditionLayer ResultConditions => GetLayer<ResultConditionLayer>("ResultCondition");
        public BuildingLayer Buildings => GetLayer<BuildingLayer>("Building");
    }
}