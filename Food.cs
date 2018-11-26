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
    /// This class contains all the method required to purchase food with mineral points, which can then be converted to energy levels. Added by Isaac Asante.
    /// </summary>
    public static class Food
    {
        private static double _mineralValue = 1; // The mineral value of 1KG of food.
        private static double _energyValue = 2; // The mineral value of 1% of energy.
        private static int _balance = 0; // The field that will store remaining points from food transactions.
        private static int _foodPurchased = 0; // The mineral points spent in the last transaction.

        public static double GetMineralValue()
        {
            return Math.Round(_mineralValue, 2);
        }

        public static double GetEnergyValue()
        {
            return _energyValue;
        }

        public static int GetBalance()
        {
            return _balance;
        }

        public static void SetBalance(int balance)
        {
            _balance = balance;
        }

        public static int GetFoodPurchased()
        {
            return _foodPurchased;
        }

        public static void ResetFoodPurchased()
        {
            _foodPurchased = 0;
        }

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

        public static int GetMaximumFoodToPurchase(Inventory inventory)
        {
            // Take into account any available balance from previous food purchases.
            return (int)Math.Floor((inventory.GetMineralPoints() + _balance) / _mineralValue);
        }

        public static void IncreaseMineralValue()
        {
            if (_mineralValue < 15) // IA - Limit the maximum exchange rate to 15 points.
            {
                _mineralValue += GameLevel.GetLevel() / 1.5;
            }
        }

        public static void DecreaseEnergyValue()
        {
            if (_energyValue <= 10)
            {
                _energyValue += _mineralValue - GameLevel.GetLevel();
            }
        }

        /// <summary>
        /// This returns the amount of energy in percentages that will be gained by converting the food purchased.
        /// </summary>
        /// <returns></returns>
        public static double FoodConvertableToEnergy()
        {
            return _foodPurchased / _energyValue;
            // IA - the _foodPurchased value is measured in mineral points.
        }

        /// <summary>
        /// This method returns the amount of energy in percentages needed to reach the full energy level of 100%. 
        /// </summary>
        /// <returns></returns>
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

        public static int FoodNeededInKG()
        {
            return (int) Math.Ceiling(EnergyNeededInMineralPoints() / _mineralValue);
        }

        public static void PurchaseFood(Inventory inventory, int convertedAmount)
        {
            _foodPurchased = convertedAmount; // store the amount for conversion to energy
            inventory.DeductPointsForFood(convertedAmount);

            double validPercentage = FoodConvertableToEnergy();
            MetaHandler.IncreaseEnergy(validPercentage);

        }

        public static void PurchaseFoodFromBalance(int convertedAmount)
        {
            GetMineralValue();
            // int foodKGAvailable = 0;
            if (convertedAmount <= _balance)
            {
               // foodKGAvailable =  (int) Math.Floor(_balance / convertedAmount);

                _balance -= convertedAmount;
            }
        }


    }
}
