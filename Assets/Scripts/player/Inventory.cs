using System.Collections.Generic;
using game;
using store.logic;
using UnityEngine;

namespace player
{
    public class Inventory
    {
        
        private Dictionary<Item, int> items = new Dictionary<Item, int>();

        private int balance = 100_000;
        
        public Dictionary<Item, int> getItems()
        {
            return items;
        }
        
        public int getBalance()
        {
            return balance;
        }

        public int adjustBalance(int amount, bool runAllUpdateBalanceLogic = true)
        {
            balance += amount;
            if (runAllUpdateBalanceLogic)
            {
                GameLogic.balanceWasUpdated = true;
            }
            return balance;
        }

        public int addItem(Item item, int amount = 1)
        {
            if (items.ContainsKey(item))
            {
                items[item] += amount;
            }
            else
            {
                items.Add(item, amount);
            }

            return items[item];
        }

        public void removeItem(Item item, int amount = 1)
        {
            if (items.ContainsKey(item) && items[item] > amount)
            {
                items[item] -= amount;
            }
            else
            {
                items.Remove(item);
            }
        }

        public void logInventory()
        {
            Debug.Log($"Inventory: {balance}, {items}");
            foreach (KeyValuePair<Item, int> entry in items)
            {
                Debug.Log($"Key: {entry.Key.getName()}, Value: {entry.Value}");
            }
        }
        
    }
}