using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Core
{
    public abstract class ViewManager
    {

        private readonly ViewActivator _viewActivator = new ViewActivator();

        public void SetChangeView(Action<object> value)
        {
            _viewActivator.OnChangeView = value;
        }

        protected void ChangeView(object value)
        {
            _viewActivator.ChangeView(value);
        }

        public void ShowLastView()
        {
            _viewActivator.ShowLastView();
        }

    }
}
