using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class ViewActivator
    {
        private object _lastView;
        private object _currentView;

        public Action<object> OnChangeView { get; set; }

        public void ChangeView(object value)
        {
            _lastView = _currentView;
            _currentView = value;
            OnChangeView(value);
        }

        public void ShowLastView()
        {
            ChangeView(_lastView);
        }

    }
}
