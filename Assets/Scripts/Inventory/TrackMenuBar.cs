using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Inventory
{
    public class TrackMenuBar : MonoBehaviour
    {
        [SerializeField] Image bar;
        [SerializeField] Image barMask;
        [SerializeField] Color acquiredColor;
        [SerializeField] Color unacquiredColor;
        [SerializeField] Image itemImage;
        [SerializeField] Text itemName;
        Item item;

        static int currentIndex=0;
        int index = 0;
        private void Awake()
        {
            index = currentIndex++;
        }

        public TrackMenuBar SetItem(Item item)
        {
            this.item = item;
            UpdateUI();
            return this;
        }

        public void UpdateUI()
        {
            bar.color = item.BarColor;
            itemImage.sprite = item.Icon;
            itemName.text = item.Name;
            itemName.color = item.TextColor;

            if (item.isAcquired)
                barMask.color = acquiredColor;
            else 
                barMask.color = unacquiredColor;
        }
    }
}