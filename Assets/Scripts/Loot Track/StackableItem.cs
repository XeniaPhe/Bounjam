using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootTrack
{
    [CreateAssetMenu(menuName ="Inventory/Stackable Item",fileName ="New Item",order =0)]
    public class StackableItem : Item
    {

        [SerializeField] int stackSize;
        public int StackSize { get => stackSize; }

        [SerializeField] int amount;
        public int Amount { get => amount; }

        public int Add(int amount)
        {
            if (amount < 0)
                Debug.LogWarning("There's no such thing as negative amount of items!");
            else if (amount + this.amount > stackSize)
            {
                amount += this.amount;
                this.amount = stackSize;
                return amount - stackSize;
            }
            else
                this.amount += amount;

            return 0;
        }

        public int Remove(int amount)
        {
            if (amount < 0)
                Debug.LogWarning("There's no such thing as negative amount of items!");
            else if (this.amount-amount < 1)
            {
                amount -= this.amount;
                this.amount = 0;
                return amount;
            }
            else
                this.amount-=amount;

            return 0;
        }

        private void OnEnable()
        {
            amount = 0;
        }
    }
}