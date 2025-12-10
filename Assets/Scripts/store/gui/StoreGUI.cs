using System;
using System.Collections.Generic;
using game;
using store.logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace store.gui
{
    public class StoreGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject materialsItemList;
        [SerializeField]
        private GameObject upgradesItemList;

        public Button ItemHANDLEButton;
        public Button ItemIRONButton;
        public Button ItemCOALButton;
        public Button ItemUPGRADE_HAMMERButton;
        public Button ItemUPGRADE_ANVILButton;
        public Button ItemUPGRADE_FORGEButton;

        public TextMeshProUGUI ItemHANDLEPrice;
        public TextMeshProUGUI ItemIRONPrice;
        public TextMeshProUGUI ItemCOALPrice;
        public TextMeshProUGUI ItemUPGRADE_HAMMERPrice;
        public TextMeshProUGUI ItemUPGRADE_ANVILPrice;
        public TextMeshProUGUI ItemUPGRADE_FORGEPrice;

        public TextMeshProUGUI ItemHANDLEAmount;
        public TextMeshProUGUI ItemIRONAmount;
        public TextMeshProUGUI ItemCOALAmount;
        public TextMeshProUGUI ItemUPGRADE_HAMMERAmount;
        public TextMeshProUGUI ItemUPGRADE_ANVILAmount;
        public TextMeshProUGUI ItemUPGRADE_FORGEAmount;

        public TextMeshProUGUI balanceMaterials;
        public TextMeshProUGUI balanceUpgrades;
        
        private Dictionary<Item, int> upgradesBought = new Dictionary<Item, int>();
        
        private Dictionary<Item, StoreItemGUI> ItemsToGUIs = new Dictionary<Item, StoreItemGUI>();
        
        private void Start()
        {
            UnityEngine.Debug.Log("Initializing StoreGUI");

            ItemHANDLEPrice.text = $"{Item.HANDLE.getCurrentPrice()} coins";
            ItemIRONPrice.text = $"{Item.IRON.getCurrentPrice()} coins";
            ItemCOALPrice.text = $"{Item.COAL.getCurrentPrice()} coins";
            ItemUPGRADE_HAMMERPrice.text = $"{Item.UPGRADE_HAMMER.getCurrentPrice()} coins";
            ItemUPGRADE_ANVILPrice.text = $"{Item.UPGRADE_ANVIL.getCurrentPrice()} coins";
            ItemUPGRADE_FORGEPrice.text = $"{Item.UPGRADE_FORGE.getCurrentPrice()} coins";
            
            upgradesBought.Add(Item.UPGRADE_HAMMER, 0);
            upgradesBought.Add(Item.UPGRADE_ANVIL, 0);
            upgradesBought.Add(Item.UPGRADE_FORGE, 0);
            
            ItemsToGUIs.Add(Item.HANDLE, new StoreItemGUI(Item.HANDLE, ItemHANDLEButton, ItemHANDLEPrice, ItemHANDLEAmount));
            ItemsToGUIs.Add(Item.COAL, new StoreItemGUI(Item.COAL, ItemCOALButton, ItemCOALPrice, ItemCOALAmount));
            ItemsToGUIs.Add(Item.IRON, new StoreItemGUI(Item.IRON, ItemIRONButton, ItemIRONPrice, ItemIRONAmount));
            ItemsToGUIs.Add(Item.UPGRADE_ANVIL, new StoreItemGUI(Item.UPGRADE_ANVIL, ItemUPGRADE_ANVILButton, ItemUPGRADE_ANVILPrice, ItemUPGRADE_ANVILAmount));
            ItemsToGUIs.Add(Item.UPGRADE_FORGE, new StoreItemGUI(Item.UPGRADE_FORGE, ItemUPGRADE_FORGEButton, ItemUPGRADE_FORGEPrice, ItemUPGRADE_FORGEAmount));
            ItemsToGUIs.Add(Item.UPGRADE_HAMMER, new StoreItemGUI(Item.UPGRADE_HAMMER, ItemUPGRADE_HAMMERButton, ItemUPGRADE_HAMMERPrice, ItemUPGRADE_HAMMERAmount));

            adjustBalance(0); // Initialize labels correctly

        }

        private void Update()
        {
            if (GameLogic.balanceWasUpdated)
            {
                GameLogic.balanceWasUpdated = false;
                updateButtonsWithBalance();
            }
        }

        private void adjustBalance(int amount)
        {
            int current_balance = GameLogic.player.getInventory().adjustBalance(amount, false);
            balanceMaterials.text = $"{current_balance}";
            balanceUpgrades.text = $"{current_balance}";
            updateButtonsWithBalance();
        }

        public void updateButtonsWithBalance()
        {
            foreach (StoreItemGUI sig in ItemsToGUIs.Values)
            {
                if (sig.item.getCurrentPrice() > GameLogic.player.getInventory().getBalance())
                {
                    if (sig.buyButton.interactable)
                    {
                        sig.buyButton.interactable = false;
                    }
                }
                else
                {
                    if (!sig.buyButton.interactable)
                    {
                        sig.buyButton.interactable = true;
                    }
                }
            }
        }

        private void buyItem(Item item)
        {
            TextMeshProUGUI price = ItemsToGUIs[item].price;
            TextMeshProUGUI amount = ItemsToGUIs[item].amount;
            int current_price = item.getCurrentPrice();
            if (item.getType() == Item.ItemType.Upgrade)
            {
                // int current_price = (int)Math.Pow(item.getDefaultPrice(), upgradesBought[item] + 1);
                if (GameLogic.player.getInventory().getBalance() >= current_price)
                {
                    GameLogic.player.getInventory().addItem(item);
                    upgradesBought[item]++;
                    int newPrice = (int)Math.Pow(item.getDefaultPrice(), upgradesBought[item] + 1);
                    price.text = $"{newPrice} coins";
                    item.setCurrentPrice(newPrice);
                    adjustBalance(-current_price);
                    amount.text = $"{upgradesBought[item]}";
                }
            }
            else
            {
                // int current_price = item.getDefaultPrice();
                if (GameLogic.player.getInventory().getBalance() >= current_price)
                {
                    int item_amount = GameLogic.player.getInventory().addItem(item);
                    adjustBalance(-current_price);
                    amount.text = $"{item_amount}";
                }
            }

            // GameLogic.player.getInventory().logInventory();
        }

        public void buyHandle()
        {
            Debug.Log("Buy Handle");
            buyItem(Item.HANDLE);
        }

        public void buyCoal()
        {
            Debug.Log("Buy Coal");
            buyItem(Item.COAL);
        }

        public void buyIron()
        {
            Debug.Log("Buy Iron");
            buyItem(Item.IRON);
        }

        public void buyUpgradeAnvil()
        {
            Debug.Log("Buy UpgradeAnvil");
            buyItem(Item.UPGRADE_ANVIL);
            
        }

        public void buyUpgradeForge()
        {
            Debug.Log("Buy UpgradeForge");
            buyItem(Item.UPGRADE_FORGE);
        }

        public void buyUpgradeHammer()
        {
            Debug.Log("Buy UpgradeHammer");
            buyItem(Item.UPGRADE_HAMMER);
        }
    }
}