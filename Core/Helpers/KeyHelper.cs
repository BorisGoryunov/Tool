using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Helpers
{
    public static class KeyHelper
    {
        private static readonly IDictionary<Key, int> _digits = new Dictionary<Key, int>()
        {
            { Key.D0, 0 },
            { Key.D1, 1 },
            { Key.D2, 2 },
            { Key.D3, 3 },
            { Key.D4, 4 },
            { Key.D5, 5 },
            { Key.D6, 6 },
            { Key.D7, 7 },
            { Key.D8, 8 },
            { Key.D9, 9 },

            { Key.NumPad0, 0 },
            { Key.NumPad1, 1 },
            { Key.NumPad2, 2 },
            { Key.NumPad3, 3 },
            { Key.NumPad4, 4 },
            { Key.NumPad5, 5 },
            { Key.NumPad6, 6 },
            { Key.NumPad7, 7 },
            { Key.NumPad8, 8 },
            { Key.NumPad9, 9 },

        };


        public static bool IsDigit(Key key)
        {
            return (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9);
        }

        public static int AsDigit(Key key)
        {
            return _digits[key];
        }

    }
}
