using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater.Common
{
    public class ConnectionStringItem : Core.ViewModel
    {

        private bool _isSelected;


        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }
}
