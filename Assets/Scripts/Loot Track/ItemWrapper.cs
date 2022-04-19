using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootTrack
{
    public class ItemWrapper : MonoBehaviour
    {
        [SerializeField] Item item;
        public Item Item { get => item; }
    }
}