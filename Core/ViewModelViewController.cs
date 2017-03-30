using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core
{
    public abstract class ViewModelViewController<TController, TViewModel, TView>
            where TViewModel : IViewModel
            where TView : ContentControl, new()
            where TController : ViewModelViewController<TController, TViewModel, TView>, new()
    {


        public static TController Create()
        {
            var view = new TView();
            if (view.DataContext == null)
            {
                throw new NullReferenceException("DataContext is null.");
            }
            var controller = new TController();
            controller.Model = (TViewModel)view.DataContext;
            controller.View = view;
            controller.Model.Initialize();
            controller.Model.View = view;
            controller.Initialize();
            return controller;
        }

        protected virtual void Initialize()
        {

        }

        public TView View { get; private set; }

        public TViewModel Model { get; private set; }
    }
}
