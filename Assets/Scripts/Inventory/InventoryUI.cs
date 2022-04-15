using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        static InventoryUI instance;
        public static InventoryUI Instance { get => instance; }

        [SerializeField] GameObject sampleSlot;
        [SerializeField] Transform slotParent;

        List<InventorySlot> slots;

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

        private void Start()
        {
            Inventory.Instance.Start();
            FetchSlots();
        }

        public void Open()
        {
            gameObject.SetActive(true);
            if (slots.Count != Inventory.Instance.items.Count)
                FetchSlots();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        void FetchSlots()
        {
            slots = new List<InventorySlot>();
            foreach (var item in Inventory.Instance.items)
                slots.Add(Instantiate(sampleSlot, slotParent).GetComponent<InventorySlot>().SetItem(item));
        }

        public void RemoveSLot(int index)
        {
            var slot = slots[index];
            slots.RemoveAt(index);
            Destroy(slot.gameObject);

            for (; index < slots.Count; index++)
            {
                slots[index].DecrementIndex();
            }
        }
    }
}