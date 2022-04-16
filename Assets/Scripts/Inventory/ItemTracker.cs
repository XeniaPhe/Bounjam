using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class ItemTracker : MonoBehaviour
    {
        static ItemTracker instance;
        public static ItemTracker Instance { get => instance; }

        public List<Item> requiredItems;
        bool isStarted;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
        }

        public void Start()
        {
            if (isStarted) return;

            foreach (var item in requiredItems)
            {
                item.isAcquired = true;
            }

            isStarted = true;
        }
        public bool UpdateItem(Item newItem)
        {
            if (newItem == null)
                return false;

            for (int i = 0; i < requiredItems.Count; i++)
            {
                if(requiredItems[i].ID == newItem.ID)
                {
                    requiredItems[i].isAcquired = true;
                    ItemTrackerUI.Instance.UpdateBar(i);
                    break;
                }
            }

            return true;
        }
    }
}