using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater.Common
{
    static class AppCore
    {
        public static ConfigManager ConfigManager { get; } = new ConfigManager();
    }
}
