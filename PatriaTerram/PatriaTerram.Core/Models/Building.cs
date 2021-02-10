using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Models
{
    public class Building
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        public int Value { get; set; }

        public List<EnvironmentCondition> EnvironmentConditions { get; set; }
    }
}
