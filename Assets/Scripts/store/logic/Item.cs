namespace store.logic
{
    public class Item
    {

        private string name;
        private float price;
        
        public Item(string name, float price)
        {
            this.name = name;
            this.price = price;
        }

        public string getName()
        {
            return name;
        }

        public float getPrice()
        {
            return price;
        }
        
    }
}