using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escapade;
using Escapade.item;

namespace Escapade
{
    /// <summary>
    /// This class contains all the method required to purchase food with mineral points, which can then be converted to energy levels.
    /// </summary>
    public static class Food
    {
        private static double _mineralValue = 1;
        private static double _energyValue = 2;
        private static int _balance = 0;
        private static int _foodPurchased = 0;

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
            if (_energyValue >= 0.1)
            {
                _energyValue -= _mineralValue - GameLevel.GetLevel();
            }
        }

        /// <summary>
        /// This returns the amount of energy points that will be gained by converting the food purchased. It is recommended to always call the UpdateFoodMaximum() method before this one, to make sure that the value for the maximum amount of food that can be purchased with mineral points first is accurate. 
        /// </summary>
        /// <returns></returns>
        public static double FoodConvertableToEnergy()
        {
            return _foodPurchased / _energyValue;
        }

        public static void PurchaseFood(Inventory inventory, int convertedAmount)
        {
            _foodPurchased = convertedAmount; // store the amount for conversion to energy
            inventory.DeductPointsForFood(convertedAmount);
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
