using PatriaTerram.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Helpers
{
    public static class RangeHelper
    {
        public static int GetAbsRange(this Range range)
        {
            return Math.Abs(range.Top - range.Bottom);
        }

        public static int ToRangeValue(this Range range, int value)
        {
            if(value >= range.Top)
            {
                return range.Top - range.Bottom;
            }

            if (value <= range.Bottom)
            {
                return 0;
            }

            return value - range.Bottom;
        }

        public static double ToRangeValuePercent(this Range range, int value)
        {
            double rangeValue = range.ToRangeValue(value);

            var distance = range.GetAbsRange();

            if(distance == 0) 
            {
                return 0;
            }

            double percent = rangeValue / distance;

            return percent;
        }
    }
}
