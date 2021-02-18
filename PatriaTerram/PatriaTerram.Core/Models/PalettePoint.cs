using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatriaTerram.Core.Models
{
    public class PalettePoint
    {
        private List<ILayer> _layers;

        //public Dictionary<string, PalettePointTerrain> Terrains { get; }
        //public Dictionary<string, BuildingCondition> BuildingConditions { get; }
        //public Dictionary<string, Building> Buildings { get; }

        public PalettePoint()
        {
            //Terrains = new Dictionary<string, PalettePointTerrain>();
            //BuildingConditions = new Dictionary<string, BuildingCondition>();
            //Buildings = new Dictionary<string, Building>();

            _layers = new List<ILayer>();

            _layers.Add(new TerrainLayer());
            _layers.Add(new BuildingConditionLayer());
            _layers.Add(new BuildingLayer());
        }

        //public void AddBuildingConditions(string buildingType, string terrain, int value)
        //{
        //    if (BuildingConditions.Keys.Contains(buildingType) == false)
        //    {
        //        var newCondition = new BuildingCondition
        //        {
        //            BuildingType = buildingType
        //        };

        //        BuildingConditions.Add(newCondition.BuildingType, newCondition);
        //    }

        //    BuildingConditions[buildingType].AddConditionValue(terrain, value);
        //}

        //public int GetBuildingConditionValue(string buildingType, string terrain)
        //{
        //    if (BuildingConditions.Keys.Contains(buildingType) == false) { return 0; }

        //    return BuildingConditions[buildingType].EnvironmentConditionValues[terrain];
        //}

        //public ILayer this[string layerName]
        //{
        //    get 
        //    {
        //        return _layers.FirstOrDefault(a => a.Name == layerName); 
        //    }
        //}

        public T GetLayer<T>(string layerName) where T : ILayer
        {
            var layer = _layers.FirstOrDefault(a => a.Name == layerName);

            if (layer == null)
            {
                return default;
            }

            return (T)layer;
        }

        public TerrainLayer Terrains => GetLayer<TerrainLayer>("Terrain");
        public BuildingConditionLayer BuildingConditions => GetLayer<BuildingConditionLayer>("BuildingCondition");
        public BuildingLayer Buildings => GetLayer<BuildingLayer>("Building");
    }
}