using AStarAlgorithm.Entityes;
using PatriaTerram.Core.Configurations;
using PatriaTerram.Core.Configurations.Entityes;
using PatriaTerram.Core.Enums;
using PatriaTerram.Core.Helpers;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Conditions.Resolvers
{
    public abstract class BasePointResolver
    {
        protected Palette _palette;

        public BasePointResolver(Palette palette)
        {
            _palette = palette;
        }

        protected int GetConditionValue(EnvironmentConditionBase environmentCondition, Coord baseCoord, Coord adjacentCoord)
        {
            int value = 0;

            switch (environmentCondition.Type)
            {
                case EnvironmentConditionType.LinearDecrease:
                    value = ConditionsResolverHelper.GetValueLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.OneLevel:
                    value = ConditionsResolverHelper.GetValueOneLevel();
                    break;
                case EnvironmentConditionType.RingOneLevel:
                    value = ConditionsResolverHelper.GetValueRingOneLevel(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                case EnvironmentConditionType.RingLinearDecrease:
                    value = ConditionsResolverHelper.GetValueRingLinearDecrease(baseCoord, adjacentCoord, environmentCondition.Radius, environmentCondition.InnerRadius, _palette.Width, _palette.Height);
                    break;
                default:
                    break;
            }


            if (environmentCondition.IsPositive == false)
            {
                value *= -1;
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
