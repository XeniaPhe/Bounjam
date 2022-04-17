using System;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item",order =0)]
    public class Item : ScriptableObject
    {
        #region Fields

        [SerializeField] protected new string name = "Item";
        public string Name { get => name; }

        [SerializeField] protected uint id;
        public uint ID { get => id; }

        [SerializeField] protected string description = "This is an item.";
        public string Description { get => description; }

        [SerializeField] protected Sprite icon;
        public Sprite Icon { get => icon; }

        [SerializeField] Color barColor;
        public Color BarColor { get => barColor; }

        [SerializeField] Color textColor;
        public Color TextColor { get => textColor; }

        public bool isAcquired = false;
        #endregion
    }
}