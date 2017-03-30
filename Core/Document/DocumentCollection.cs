using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.Document
{
    public class DocumentCollection : ViewModel
    {
        private readonly IList<DocumentCollectionItem> _items = new ObservableCollection<DocumentCollectionItem>();
        private int _selectedIndex = 0;
        private DocumentCollectionItem _selectedItem;



        public DocumentCollectionItem Add(DocumentCollectionItem item, bool activated)
        {
            _items.Add(item);
            if (activated)
            {
                SelectedIndex = _items.Count - 1;
            }
            item.OnClose += Remove;
            return item;
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var item in _items)
            {
                item.Initialize();
            }
        }

        public void Add(DocumentCollectionItem item)
        {
            Add(item, true);
        }

        public void Remove(DocumentCollectionItem item)
        {
            _items.Remove(item);
        }

        public IList<DocumentCollectionItem> Items
        {
            get
            {
                return _items;
            }
        }


        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                _selectedIndex = value;
                SelectedItem = Items[value];
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }

        public DocumentCollectionItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            private set
            {
                _selectedItem = value;
            }
        }
    }
}
