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
            var coloredComponents = Components.Where(a => a.Terrain.IsAffectColor);

            r = GetAvarageField(coloredComponents, a => a.Terrain.ColorR);
            g = GetAvarageField(coloredComponents, a => a.Terrain.ColorG);
            b = GetAvarageField(coloredComponents, a => a.Terrain.ColorB);
        }

        private int GetAvarageField<T>(IEnumerable<T> items, Func<T, int> field)
        {
            var sum = 0;
            var count = 0;

            foreach (var item in items)
            {
                sum += field(item);
                count++;
            }

            return sum / count;
        }
    }
}