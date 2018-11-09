using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
    public static class GameLevel
    {
        private static int levelNo = 1;

        public static void IncreaseLevel()
        {
            levelNo++;
        }

        public static void DecreaseLevel()
        {
            levelNo--;
        }

        public static String PrintLevel()
        {
            return levelNo.ToString();
        }
    }
}
