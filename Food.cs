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
        private static int _maximumFoodToPurchase = 0;
        private static int _balance = 0;

        public static double GetMineralValue()
        {
            return _mineralValue;
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

        public static void IncreaseMineralValue()
        {
            _mineralValue += GameLevel.GetLevel() / 2;
        }

        public static void DecreaseEnergyValue()
        {
            if (_energyValue >= 0.1)
            {
                _energyValue -= _mineralValue - GameLevel.GetLevel();
            }
        }

        public static void UpdateFoodMaximum(Inventory inventory)
        {
            // Take into account any available balance from previous food purchases.
            _maximumFoodToPurchase = (int) Math.Floor((inventory.GetMineralPoints() + _balance) / _mineralValue);
        }

        /// <summary>
        /// This returns the amount of energy points that will be gained by converting the food purchased. It is recommended to always call the UpdateFoodMaximum() method before this one, to make sure that the value for the maximum amount of food that can be purchased with mineral points first is accurate. 
        /// </summary>
        /// <returns></returns>
        public static double FoodConvertableToEnergy()
        {
            return _maximumFoodToPurchase / _energyValue;
        }

        public static void PurchaseFood(Inventory inventory, int requestedAmount)
        {
            UpdateFoodMaximum(inventory); // IA - verify how much food can be purchased first.
            
            if (_maximumFoodToPurchase > 0) // IA - if the player can purchase food
            {
                inventory.DeductMineralPoints(requestedAmount);
            }
        }
    }
}
