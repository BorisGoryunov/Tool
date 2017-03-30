using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseUpdater.Common
{
    public class ConnectionStringCollection : Core.CollectionViewModel<ConnectionStringItem>
    {
        private readonly ICommand _selectAllCommand;


        public ConnectionStringCollection()
        {
            _selectAllCommand = new Core.CommandHandler(SelectAll);
        }

        protected override void Load()
        {
            var items = AppCore.ConfigManager.GetConnectionStrings();
            foreach (var item in items)
            {
                var connectionStringItem = new ConnectionStringItem();
                connectionStringItem.Name = item.Name;
                connectionStringItem.ConnectionString = item.ConnectionString;
                Add(connectionStringItem);
            }
        }

        private void SelectAll(object obj)
        {
            foreach (var item in Items)
            {
                item.IsSelected = true;
            }
        }

        public ICommand SelectAllCommand
        {
            get
            {
                return _selectAllCommand;
            }
        }
    }
}
