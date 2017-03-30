using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Document
{
    public abstract class DocumentCollectionItem : ViewModel
    {
        private readonly CommandHandler _closeCommand;

        private bool _isEnabled = true;
        private bool _allowClose = true;
        private string _header;
        public event Action<DocumentCollectionItem> OnClose = delegate { };
        public event Action<DocumentCollectionItem> OnHided = delegate { };


        public DocumentCollectionItem()
        {
            _closeCommand = new CommandHandler(Close);
        }

        protected virtual void Close(object obj)
        {
            OnClose(this);
        }


        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand;
            }
        }

        public virtual void Activate()
        {

        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        public bool AllowClose
        {
            get
            {
                return _allowClose;
            }

            set
            {
                _allowClose = value;
                RaisePropertyChanged(nameof(AllowClose));
            }
        }

        public string Header
        {
            get
            {
                return _header;
            }

            protected set
            {
                _header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }
    }
}
