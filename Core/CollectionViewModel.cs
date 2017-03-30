using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core
{
    public abstract class CollectionViewModel<TItem> : ViewModel
    {
        private TItem _selectedItem;
        private IEnumerable<TItem> _items;
        private IList<TItem> _data;
        private readonly ICommand _loadCommand;

        public CollectionViewModel()
        {
            _loadCommand = new CommandHandler(LoadItems);
        }

        protected abstract void Load();

        public void LoadItems()
        {
            LoadItems(null);
        }

        private void LoadItems(object obj)
        {
            _data = new List<TItem>();
            Load();
            Items = _data;
            SelectedItem = Items.FirstOrDefault();
        }

        protected void Add(TItem item)
        {
            _data.Add(item);
        }


        public TItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public IEnumerable<TItem> Items
        {
            get
            {
                return _items;
            }

            protected set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                return _loadCommand;
            }
        }
    }
}
