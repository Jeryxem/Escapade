using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escapade
{
	/// <summary>
	/// This class holds all the methods necessary to control the game's level.
	/// </summary>
    public static class GameLevel
    {
        private static int _levelNo = 1;
        private static int _formula;

		/// <summary>
		/// Gets the actual game level.
		/// </summary>
		/// <returns>The game level as an integer.</returns>
        public static int GetLevel()
        { return _levelNo; }

		/// <summary>
		/// Sets the game level. 
		/// </summary>
		/// <param name="value">Value.</param>
        public static void SetLevel(int value)
        { _levelNo = value; }

		/// <summary>
		/// Gets the level's own formula, which is used to determine how many enemies must be destroyed at every level.
		/// </summary>
		/// <returns>The formula.</returns>
        public static int GetFormula()
        {
            _formula = (_levelNo * 5) + 1 + (_levelNo * _levelNo); 
			// IA - The result of this formula must be compared to the count of enemies destroyed at every level to decide level progression.
            return _formula;
        }

		/// <summary>
		/// Increases the current level by one.
		/// </summary>
        public static void IncreaseLevel()
        {
            _levelNo++;
        }

		/// <summary>
		/// Resets the game's level to 1. 
		/// This method may be called whenever a new game starts, without the application being shut down.
		/// </summary>
        public static void ResetLevel()
        {
            _levelNo = 1;
        }

		/// <summary>
		/// Display the current level.
		/// </summary>
		/// <returns>The current level as a string.</returns>
        public static String PrintLevel()
        {
            return _levelNo.ToString();
        }
    }
}
