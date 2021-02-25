using PatriaTerram.Game.Entityes;
using System.Collections.Generic;
using PatriaTerram.Game.Enums;
using PatriaTerram.Core.Condition.Enums;

namespace PatriaTerram.Game
{
    public class StepFactory
    {
        public List<Step> GetStartedPack(string townName)
        {
            var townHall = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.TownHall,
                TownName = townName
            };
            var sawmill = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Sawmill,
                TownName = townName
            };
            var farm = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Farm,
                TownName = townName
            };
            var house = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.House,
                TownName = townName
            };
            var stonepit = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Stonepit,
                TownName = townName
            };
            var mill = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Mill,
                TownName = townName
            };
            var market = new Step
            {
                Action = StepAction.Build,
                BuildingType = BuildingType.Market,
                TownName = townName
            };

            var startedPack = new List<Step>()
            {
                townHall,
                sawmill,
                sawmill,
                sawmill,
                stonepit,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                farm,
                house,
                house,
                house,
                house,
                house,
                house,
                house,
            };

            return startedPack;
        }

        private Step GetBuildStep(BuildingType buildingType, string town)
        {
            return new Step
            {
                Action = StepAction.Build,
                BuildingType = buildingType,
                TownName = town
            };
        }

        private List<Step> GetBuildStep(BuildingType buildingType, string town, int count)
        {
            var steps = new List<Step>();

            for (int i = 0; i < count; i++)
            {
                steps.Add(GetBuildStep(buildingType, town));
            }

            return steps;
        }

        public List<Step> GetFarmVillage(string town)
        {
            var steps = new List<Step>();

            steps.Add(GetBuildStep(BuildingType.FarmTownHall, town));

            steps.AddRange(GetBuildStep(BuildingType.Sawmill, town, 2));

            steps.AddRange(GetBuildStep(BuildingType.House, town, 11));

            steps.Add(GetBuildStep(BuildingType.Mill, town));
            steps.AddRange(GetBuildStep(BuildingType.Farm, town, 10));

            steps.Add(GetBuildStep(BuildingType.Mill, town));
            steps.AddRange(GetBuildStep(BuildingType.Farm, town, 10));

            steps.Add(GetBuildStep(BuildingType.Mill, town));
            steps.AddRange(GetBuildStep(BuildingType.Farm, town, 10));

            steps.Add(GetBuildStep(BuildingType.Market, town));

            return steps;
        }

        public List<Step> GetStonepitVillage(string town)
        {
            var steps = new List<Step>();

            steps.Add(GetBuildStep(BuildingType.StonepitTownHall, town));

            steps.AddRange(GetBuildStep(BuildingType.Stonepit, town, 5));

            steps.AddRange(GetBuildStep(BuildingType.House, town, 5));

            steps.AddRange(GetBuildStep(BuildingType.Sawmill, town, 2));

            steps.Add(GetBuildStep(BuildingType.Market, town));

            return steps;
        }

        public List<Step> GetCapitalVillage(string town)
        {
            var steps = new List<Step>();

            steps.Add(GetBuildStep(BuildingType.TownHall, town));

            steps.AddRange(GetBuildStep(BuildingType.House, town, 10));

            steps.Add(GetBuildStep(BuildingType.Market, town));

            steps.AddRange(GetBuildStep(BuildingType.House, town, 15));

            return steps;
        }

        public List<Step> GetTwoKingdoms()
        {
            var steps = new List<Step>();

            steps.AddRange(GetStartedPack("Farm_1"));
            steps.AddRange(GetStartedPack("Farm_2"));

            return steps;
        }

        public List<Step> GetBigCounty()
        {
            var steps = new List<Step>();

            steps.AddRange(GetFarmVillage("Farm_Village_1"));
            steps.AddRange(GetFarmVillage("Farm_Village_2"));
            steps.AddRange(GetFarmVillage("Farm_Village_3"));
            steps.AddRange(GetStonepitVillage("Stonepit_Village"));
            steps.AddRange(GetCapitalVillage("Capital"));

            return steps;
        }
    }
}
