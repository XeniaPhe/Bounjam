using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] Text amountText;
        [SerializeField] Button deleteButton;
        Item item;

        static int currentIndex=0;
        int index = 0;
        private void Awake()
        {
            index = currentIndex++;
            deleteButton.onClick.AddListener(DeleteItem);
        }

        public InventorySlot SetItem(Item item)
        {
            if(item)
            {
                //Dunno what happens in that case yet
            }

            this.item = item;
            UpdateUI();
            return this;
        }

        public void DeleteItem()
        {
            Inventory.Instance.RemoveItem(item);
            --currentIndex;
            InventoryUI.Instance.RemoveSLot(index);
        }

        public void DecrementIndex()
        {
            --index;
        }

        public void UpdateUI()
        {
            if (item == null)
            {
                itemImage.sprite = null;
                itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0);
                amountText.text = "";
                return;
            }

            itemImage.sprite = item.Icon;
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1);

            if (item.GetType() == typeof(StackableItem))
                amountText.text = ((StackableItem)item).Amount.ToString();
            else
                amountText.text = "";
        }
    }
}