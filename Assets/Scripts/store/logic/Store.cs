using System.Collections.Generic;

namespace store.logic
{
    public class Store
    {
        
        List<Item> items;

        public void initialize()
        {
            items = new List<Item>();
            
            addItem(new Item("Test1", 10.5f));
            addItem(new Item("Test2", 5.0f));
        }

        private void addItem(Item item)
        {
            items.Add(item);
        }
        
        public List<Item> GetItems()
        {
            return items;
        }
        
        
        
    }
}