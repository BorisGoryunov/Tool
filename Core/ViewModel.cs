using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Core
{
    public abstract class ViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private object _view;


        protected virtual void RaisePropertyChanged()
        {
            RaisePropertyChanged(string.Empty);
        }

        protected virtual void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Refresh()
        {
            RaisePropertyChanged();
        }

        public virtual void Initialize()
        {

        }

        public object View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
                RaisePropertyChanged(nameof(View));
            }
        }
    }


    public abstract class DialogViewModel : ViewModel, IDisposable
    {

        private readonly ICommand _pressCancelCommand;
        private readonly ICommand _pressOkCommand;
        private readonly DispatcherTimer _closeTimer = new DispatcherTimer();

        public Action<DialogViewModel> OnPressOk { get; set; }

        public Action<DialogViewModel> OnPressCancel { get; set; }


        public DialogViewModel()
        {
            _pressOkCommand = new CommandHandler(PressOk);
            _pressCancelCommand = new CommandHandler(PressCancel);
            _closeTimer.Tick += CloseTimerTick;
        }

        private void CloseTimerTick(object sender, EventArgs e)
        {
            PressCancel(this);
        }

        protected void ResetTimer()
        {
            _closeTimer.Stop();
            _closeTimer.Start();
        }

        protected void InitializeTimer(TimeSpan interval)
        {
            _closeTimer.Interval = interval;
            _closeTimer.Start();
        }

        protected void StartTimer()
        {
            _closeTimer.Start();
        }

        protected void StopTimer()
        {
            _closeTimer.Stop();
        }


        public virtual void Dispose()
        {
            _closeTimer.Stop();
        }

        private void PressOk(object obj)
        {
            _closeTimer.Stop();
            OnPressOk(this);
        }

        private void PressCancel(object obj)
        {
            _closeTimer.Stop();
            OnPressCancel(this);
        }

        protected void PressKey()
        {
            ResetTimer();
        }

        public ICommand PressCancelCommand
        {
            get
            {
                return _pressCancelCommand;
            }
        }

        public ICommand PressOkCommand
        {
            get
            {
                return _pressOkCommand;
            }
        }
    }

}
