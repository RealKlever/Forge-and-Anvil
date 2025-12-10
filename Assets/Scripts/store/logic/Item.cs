using System.Collections.Generic;

namespace store.logic
{
    
    public class Item
    {

        public static readonly Item HANDLE = new Item("handle", 8, ItemType.Material);
        public static readonly Item IRON = new Item("iron", 10, ItemType.Material);
        public static readonly Item COAL = new Item("coal", 5, ItemType.Material);
        public static readonly Item UPGRADE_HAMMER = new Item("upgradehammer", 50, ItemType.Upgrade);
        public static readonly Item UPGRADE_ANVIL = new Item("upgradeanvil", 50, ItemType.Upgrade);
        public static readonly Item UPGRADE_FORGE = new Item("upgradeforge", 50, ItemType.Upgrade);
        
        //sell sword for ~30 get better when using upgrades
        
        public static readonly List<Item> MATERIALS = new List<Item> {HANDLE, IRON, COAL};
        public static readonly List<Item> UPGRADES = new List<Item> {UPGRADE_HAMMER, UPGRADE_ANVIL, UPGRADE_FORGE};

        public enum ItemType
        {
            Material,
            Upgrade,
        }
        
        private string name;
        private int defaultPrice;
        private int currentPrice;
        private ItemType type;
        
        private Item(string name, int defaultPrice, ItemType type)
        {
            this.name = name;
            this.defaultPrice = defaultPrice;
            this.currentPrice = defaultPrice;
            this.type = type;
        }

        public string getName()
        {
            return name;
        }

        public int getDefaultPrice()
        {
            return defaultPrice;
        }

        public int getCurrentPrice()
        {
            return currentPrice;
        }

        public void setCurrentPrice(int newCurrentPrice)
        {
            currentPrice = newCurrentPrice;
        }

        public ItemType getType()
        {
            return type;
        }
        
    }
}