using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
    public static class GameLevel
    {
        private static int _levelNo = 1;

        public static int GetLevel()
        { return _levelNo; }

        public static void SetLevel(int value)
        { _levelNo = value; }

        public static void IncreaseLevel()
        {
            _levelNo++;
        }

        public static void ResetLevel()
        {
            _levelNo = 1;
        }

        public static String PrintLevel()
        {
            return _levelNo.ToString();
        }
    }
}
