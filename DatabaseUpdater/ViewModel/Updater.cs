using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseUpdater.Common;
using System.Windows.Input;

namespace DatabaseUpdater.ViewModel
{
    public class Updater : Core.ViewModel
    {
        private readonly ConnectionStringCollection _connectionCollection = new ConnectionStringCollection();
        private string _pathToSqlFiles;
        private readonly ICommand _selectPathToSqlFilesCommand;
        private readonly ICommand _runCommand;

        public Updater()
        {
            _selectPathToSqlFilesCommand = new Core.CommandHandler(SelectPathToSqlFiles);
            _runCommand = new Core.CommandHandler(Run);
        }

        private void SelectPathToSqlFiles(object obj)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result==System.Windows.Forms.DialogResult.OK)
                {
                    PathToSqlFiles = dialog.SelectedPath;
                }
            }
            SaveConfig();
        }

        private void Run(object obj)
        {
            if (string.IsNullOrEmpty(PathToSqlFiles))
            {
                throw new Core.ApplicationException("Выберите путь к сприптам.");
            }
            SaveConfig();
        }

        private void SaveConfig()
        {
            AppCore.ConfigManager.Save();
        }

        public override void Initialize()
        {
            base.Initialize();
            ConnectionCollection.LoadItems();
            PathToSqlFiles = AppCore.ConfigManager.PathToSQLFiles;
        }

        public ConnectionStringCollection ConnectionCollection
        {
            get
            {
                return _connectionCollection;
            }
        }

        public string PathToSqlFiles
        {
            get
            {
                return _pathToSqlFiles;
            }

            set
            {
                _pathToSqlFiles = value;
                RaisePropertyChanged(nameof(PathToSqlFiles));
                AppCore.ConfigManager.PathToSQLFiles = PathToSqlFiles;
            }
        }

        public ICommand SelectPathToSqlFilesCommand
        {
            get
            {
                return _selectPathToSqlFilesCommand;
            }
        }

        public ICommand RunCommand
        {
            get
            {
                return _runCommand;
            }
        }
    }
}
