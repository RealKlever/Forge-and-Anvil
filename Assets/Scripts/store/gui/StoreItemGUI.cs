using store.logic;
using TMPro;
using UnityEngine.UI;

namespace store.gui
{
    public class StoreItemGUI
    {
        public Item item;
        public Button buyButton;
        public TextMeshProUGUI price;
        public TextMeshProUGUI amount;

        public StoreItemGUI(Item item, Button buyButton, TextMeshProUGUI price, TextMeshProUGUI amount)
        {
            this.item = item;
            this.buyButton = buyButton;
            this.price = price;
            this.amount = amount;
        }

    }
}