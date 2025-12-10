using System.Collections.Generic;
using store.logic;

namespace player
{
    public class Inventory
    {
        
        private List<Item> items = new List<Item>();
        
        public List<Item> getItems()
        {
            return items;
        }
        
    }
}