using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriaTerram.Core.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string info)
        {
            Console.WriteLine(info);
        }
    }
}
