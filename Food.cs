using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escapade;
using Escapade.item;
using Escapade.src.gui;

namespace Escapade
{
    /// <summary>
    /// This class contains all the methods required to purchase food with mineral points, which can then be converted to energy levels. 
	/// [Added by Isaac Asante]
    /// </summary>
    public static class Food
    {
        private static double _mineralValue = 1; // The mineral value of 1KG of food.
        private static double _energyValue = 2; // The mineral value of 1% of energy.
        private static int _balance = 0; // The field that will store remaining points from food transactions.
        private static int _foodPurchased = 0; // The mineral points spent in the last transaction.

		/// <summary>
		/// Gets the mineral value for the actual level.
		/// </summary>
		/// <returns>The mineral value, rounded off to two decimal places.</returns>
        public static double GetMineralValue()
        {
            return Math.Round(_mineralValue, 2);
        }

		/// <summary>
		/// Gets the energy value for the actual level.
		/// </summary>
		/// <returns>The energy value, as a double.</returns>
        public static double GetEnergyValue()
        {
            return _energyValue;
        }

		/// <summary>
		/// Gets the player's available balance.
		/// </summary>
		/// <returns>The balance as an integer.</returns>
        public static int GetBalance()
        {
            return _balance;
        }

		/// <summary>
		/// Sets the player's balance.
		/// </summary>
		/// <param name="balance">The balance as an integer.</param>
        public static void SetBalance(int balance)
        {
            _balance = balance;
        }

		/// <summary>
		/// Gets the worth of the food purchased in the last transaction.
		/// </summary>
		/// <returns>The worth of the food recently purchased.</returns>
        public static int GetFoodPurchased()
        {
            return _foodPurchased;
        }

		/// <summary>
		/// Reset the worth of the food purchased from the last transaction to zero. 
		/// This is useful to avoid incorrect calculations when the game is restarted, but the application is not closed.
		/// </summary>
        public static void ResetFoodPurchased()
        {
            _foodPurchased = 0;
        }

		/// <summary>
		/// Resets the mineral value to 1, just as at the beginning of the game.
		/// </summary>
        public static void ResetMineralValue()
        {
            _mineralValue = 1;
        }

		/// <summary>
		/// Resets the energy value to 2, just as at the beginning of the game.
		/// </summary>
        public static void ResetEnergyValue()
        {
            _energyValue = 2;
        }

		/// <summary>
		/// Deducts the necessary amount from the player's available balance to effect minerals-to-food transactions, when applicable.
		/// </summary>
		/// <param name="foodValue">Food value.</param>
		/// <param name="inventory">Inventory.</param>
        public static void DeductBalance(int foodValue, Inventory inventory)
        {
            if (foodValue <= _balance)
            {
                _balance -= foodValue;
            }

            if (foodValue == (inventory.GetMineralPoints() + _balance))
            {
                _balance = 0;
            }
        }

		/// <summary>
		/// Gets the maximum amount of food that the player is eligible to purchase.
		/// </summary>
		/// <returns>The maximum food to purchase.</returns>
		/// <param name="inventory">Inventory.</param>
        public static int GetMaximumFoodToPurchase(Inventory inventory)
        {
            // Take into account any available balance from previous food purchases.
            return (int)Math.Floor((inventory.GetMineralPoints() + _balance) / _mineralValue);
        }

		/// <summary>
		/// Increases the worth of food to compel the player to spend more in subsequent transactions.
		/// This method may be called when the player reaches a new level.
		/// </summary>
        public static void IncreaseMineralValue()
        {
            if (_mineralValue < 15) // IA - Limit the maximum exchange rate to 15 points.
            {
                _mineralValue += GameLevel.GetLevel() / 1.5;
            }
        }

		/// <summary>
		/// Increases the value of energy to make the player purchase less food.
		/// </summary>
        public static void IncreaseEnergyValue()
        {
            if (_energyValue <= 10)
            {
                _energyValue += _mineralValue - GameLevel.GetLevel();
            }
        }

        /// <summary>
        /// This returns the amount of energy in percentages that will be gained by converting the food purchased.
        /// </summary>
        /// <returns>The amount of energy acquired in percentages.</returns>
        public static double FoodConvertableToEnergy()
        {
            return _foodPurchased / _energyValue;
            // IA - the _foodPurchased value is measured in mineral points.
        }

        /// <summary>
        /// This method returns the amount of energy in percentages needed to reach the full energy level of 100%. 
        /// </summary>
        /// <returns>The gap of energy between the player's current level in percentages, and 100%.</returns>
        public static int EnergyNeededInPercentage()
        {
            int max = 100 - MetaHandler.GetEnergyLevel(); // IA - Current energy levels in % + what the player purchased.

            return max;
        }

        /// <summary>
        /// This method returns the amount of mineral points required to reach the maximum energy level of 100%.
        /// </summary>
        /// <returns></returns>
        public static double EnergyNeededInMineralPoints()
        {
            return EnergyNeededInPercentage() * _energyValue;
        }

		/// <summary>
		/// The volume of food required to reach top energy levels, in kilograms.
		/// </summary>
		/// <returns>The volume of food needed in kilograms.</returns>
        public static int FoodNeededInKG()
        {
            return (int)Math.Round(EnergyNeededInPercentage() * _energyValue / _mineralValue);
        }

		/// <summary>
		/// Performs minerals-to-food transactions and updates the energy level progress bar accordingly.
		/// </summary>
		/// <param name="inventory">Inventory.</param>
		/// <param name="convertedAmount">Converted amount.</param>
        public static void PurchaseFood(Inventory inventory, int convertedAmount)
        {
            _foodPurchased = convertedAmount; // store the amount for conversion to energy
            inventory.DeductPointsForFood(convertedAmount);

            double validPercentage = FoodConvertableToEnergy();
            MetaHandler.IncreaseEnergy(validPercentage);

        }

		/// <summary>
		/// Performs minerals-to-food transactions using only the player's balance
		/// </summary>
		/// <param name="convertedAmount">Converted amount.</param>
		public static void PurchaseFoodFromBalance(int convertedAmount)
		{
			GetMineralValue();
			// int foodKGAvailable = 0;
			if (convertedAmount <= _balance)
			{
				_balance -= convertedAmount;
			}

			double validPercentage = FoodConvertableToEnergy();
			MetaHandler.IncreaseEnergy(validPercentage);
		}


    }
}