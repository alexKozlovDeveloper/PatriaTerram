﻿using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Condition.Configurations.Entityes;
using PatriaTerram.Core.Condition.Enums;
using PatriaTerram.Core.Condition.Helpers;
using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;

namespace PatriaTerram.Core.Conditions.Resolvers
{
    public abstract class BasePointResolver
    {
        protected Palette<ConditionPalettePoint> _palette;

        public BasePointResolver(Palette<ConditionPalettePoint> palette)
        {
            _palette = palette;
        }

        protected int GetConditionValue(EnvironmentConditionBase environmentCondition, Coord baseCoord, Coord adjacentCoord)
        {
            int value = 0;

            switch (environmentCondition.Type)
            {
                case EnvironmentConditionType.LinearDecrease:
                    value = ResultConditionValueHelper.GetValueLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.OneLevel:
                    value = ResultConditionValueHelper.GetValueOneLevel();
                    break;
                case EnvironmentConditionType.RingOneLevel:
                    value = ResultConditionValueHelper.GetValueRingOneLevel(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.RingLinearDecrease:
                    value = ResultConditionValueHelper.GetValueRingLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                default:
                    break;
            }

            return value;
        }

        protected int GetResultValue(int conditionValue, Range range, int priority)
        {
            double value = range.ToRangeValuePercent(conditionValue);

            double res = value * priority * 100;

            return (int)res;
        }
    }
}
