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
        private static int _formula;


        public static int GetLevel()
        { return _levelNo; }

        public static void SetLevel(int value)
        { _levelNo = value; }

        public static int GetFormula()
        {
            _formula = (_levelNo * 5) + 1 + (_levelNo * _levelNo); // IA - The result of this formula must be compared to the count of enemies destroyed at every level to decide level progression.
            return _formula;
        }

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
