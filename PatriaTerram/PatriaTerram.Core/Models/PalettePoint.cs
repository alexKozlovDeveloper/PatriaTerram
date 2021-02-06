using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        public List<Component> Components { get; }

        private List<BuildingConditions> _buildingConditions;
        public IEnumerable<BuildingConditions> BuildingConditions 
        { 
            get
            {
                return _buildingConditions;
            }
        }

        public PalettePoint()
        {
            Components = new List<Component>();
            _buildingConditions = new List<BuildingConditions>();
        }

        public void AddBuildingConditions(BuildingConditions condition)
        {
            var existCondition = _buildingConditions.FirstOrDefault(a => a.BuildingType == condition.BuildingType);

            if(existCondition == null)
            {
                _buildingConditions.Add(condition);
            }
            else
            {
                existCondition.Value += condition.Value;
            }
        }

        public void GetPointColor(out int r, out int g, out int b)
        {
            r = 0;
            g = 0;
            b = 0;

            int count = 0;

            foreach (var component in Components)
            {
                if(component.Terrain.IsAffectColor == false) { continue; }

                r += component.Terrain.ColorR;
                g += component.Terrain.ColorG;
                b += component.Terrain.ColorB;

                count++;
            }

            r /= count;
            g /= count;
            b /= count;
        }
    }
}