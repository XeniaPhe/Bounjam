using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        static Inventory instance;
        public static Inventory Instance { get => instance; }

        [System.NonSerialized] public List<Item> items;
        [SerializeField] Item[] initials;
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
            items = new List<Item>();
            foreach (var item in initials)
                AddItem(Instantiate<Item>(item));
            isStarted = true;
        }
        public bool AddItem(Item newItem)
        {
            if (newItem == null)
                return false;

            items.Add(newItem);
            return true;
        }
        public bool RemoveItem(Item oldItem)
        {
            if (oldItem == null)
                return false;

            return items.Remove(oldItem);
        }
        public bool AddStackableItem(StackableItem newItem, int amount)
        {
            if (newItem == null)
                return false;

            var sameItemsList = items.Where(i => i.ID == newItem.ID).ToList().Select(i => (StackableItem)i).ToList();

            foreach (var item in sameItemsList)
            {
                if ((amount = item.Add(amount)) == 0)
                    break;
            }

            newItem.Remove(newItem.StackSize);

            while (amount > 0)
            {
                amount = newItem.Add(amount);
                items.Add(newItem);
                newItem.Remove(newItem.StackSize);
            }

            return true;
        }
        public bool RemoveStackableItem(StackableItem oldItem, int amount)
        {
            if (oldItem == null)
                return false;

            var list = items.Where(i => i.ID == oldItem.ID).ToList().Select(i => (StackableItem)i).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if ((amount = list[i].Remove(amount)) > 0)
                    list.RemoveAt(i);
                else
                    break;
            }
            return true;
        }
    }
}