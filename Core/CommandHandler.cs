using System;
using System.Windows.Input;

namespace Core
{
    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private Action<object[]> _actionWithParams;
        private bool _canExecute;
        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
                _canExecute = value;
                OnCanExecuteChanged();
            }
        }

        public CommandHandler(Action<object> action)
        {
            _action = action;
            _canExecute = true;
        }

        public CommandHandler(Action<object> action, bool isEnabled)
        {
            _action = action;
            _canExecute = isEnabled;
        }


        public CommandHandler(Action<object[]> action)
        {
            _actionWithParams = action;
            _canExecute = true;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object obj)
        {
            if (obj is Array)
            {
                _actionWithParams((object[])obj);
            }
            else
            {
                _action(obj);
            }
        }
    }
}
