using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater.ViewModel
{
    public class Main : Core.ViewModel
    {
        public string Title { get; private set; } = "DatabaseUpdater";

        public override void Initialize()
        {
            base.Initialize();
            Common.AppCore.ConfigManager.Open();
            var controller = Controller.UpdaterController.Create();
            View = controller.View;
        }
    }
}
