using Core.ConfigManager.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater.Common
{
    class ConfigManager : Core.ConfigManagerBase
    {
        [Parameter]
        public string PathToSQLFiles { get; set; }

    }
}
