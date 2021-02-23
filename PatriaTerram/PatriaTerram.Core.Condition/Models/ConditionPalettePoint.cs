using PatriaTerram.Core.Condition.Layers;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Layers;
using PatriaTerram.Core.Models;
using System.Collections.Generic;

namespace PatriaTerram.Core.Condition.Models
{
    public class ConditionPalettePoint : TerrainPalettePoint
    {
        public ConditionPalettePoint()
        {
            _layers = new List<ILayer>
            {
                new TerrainLayer(),
                new TerrainConditionLayer(),
                new BuildingConditionLayer(),                
                new ResultConditionLayer(),
                new BuildingLayer()
            };
        }

        public TerrainConditionLayer TerrainConditions => GetLayer<TerrainConditionLayer>("TerrainCondition");
        public BuildingConditionLayer BuildingConditions => GetLayer<BuildingConditionLayer>("BuildingCondition");        
        public ResultConditionLayer ResultConditions => GetLayer<ResultConditionLayer>("ResultCondition");
        public BuildingLayer Buildings => GetLayer<BuildingLayer>("Building");
    }
}