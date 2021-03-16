using PatriaTerram.Core.Condition.Models;
using PatriaTerram.Core.Interfaces;
using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Condition.Factoryes
{
    public class ConditionPalettePointFactory : IPalettePointFactory<ConditionPalettePoint>
    {
        public ConditionPalettePoint Create(int x, int y)
        {
            return new ConditionPalettePoint(x, y);
        }
    }
}
