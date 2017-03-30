using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class ComboboxSource<TItem> : ViewModel
    {
        private IList<TItem> _items = new ObservableCollection<TItem>();
        private IList<TItem> _data;
        private TItem _selectedItem;

        public string ValueMember { get; protected set; } = "Id";
        public string DisplayMember { get; protected set; } = "Name";

        public void Add(IList<TItem> items)
        {
            Items = items;
        }

        public void BeginUpdate()
        {
            _data = new ObservableCollection<TItem>();
        }
        public void EndUpdate()
        {
            Items = _data;
        }

        public void SelectFirstItem()
        {
            SelectedItem = Items.FirstOrDefault();
        }


        public void Add(TItem item)
        {
            _data.Add(item);
        }


        public IList<TItem> Items
        {
            get
            {
                return _items;
            }
            private set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
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
    }

}
