using PatriaTerram.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.BuildingConditions
{
    public interface IConditionsResolver
    {
        void ResolvePoint(Palette palette, Coord pointCoord);
        void FinalResolve(Palette palette);
    }
}
