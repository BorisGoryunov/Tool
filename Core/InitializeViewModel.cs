using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class InitializeViewModel : ViewModel
    {
        private readonly IList<InitializeItem> _items = new ObservableCollection<InitializeItem>();
        private InitializeItem _selectedItem;

        private void AddLog(string value, bool isError)
        {
            var item = new InitializeItem();
            item.Header = value;
            item.IsError = isError;
            Items.Add(item);
            _selectedItem = item;
        }

        protected void AddLog(string value)
        {
            AddLog(value, false);
        }

        protected void AddLog(string value, Action action)
        {
            AddLog(value, false);
            action();
        }

        protected void AddLogOk()
        {
            AddLog("OK", false);
        }

        public override void Initialize()
        {

        }

        protected abstract void OnInitialized();

        public bool Initialized()
        {
            try
            {
                OnInitialized();
            }
            catch (Exception ex)
            {
                AddLog(ex.ToString(), true);
                return false;
            }
            return true;
        }

        public IList<InitializeItem> Items
        {
            get
            {
                return _items;
            }
        }

    }


    public sealed class InitializeItem
    {
        public bool IsError { get; set; }
        public string Header { get; set; }

    }
}
