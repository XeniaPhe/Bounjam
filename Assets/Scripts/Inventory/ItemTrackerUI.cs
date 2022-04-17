using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Inventory
{
    public class ItemTrackerUI : MonoBehaviour
    {
        static ItemTrackerUI instance;
        public static ItemTrackerUI Instance { get => instance; }

        [SerializeField] GameObject sampleBar;
        [SerializeField] Transform barParent;

        [SerializeField] GameObject bg;
        List<TrackMenuBar> bars;

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
            FetchBars();
            Toggle();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                Toggle();
        }

        private void Toggle()
        {
            bg.SetActive(!bg.activeSelf);
            ItemDetailsMenu.Instance.Unload();
        }

        void FetchBars()
        {
            bars = new List<TrackMenuBar>();
            foreach (var item in ItemTracker.Instance.requiredItems)
                bars.Add(Instantiate(sampleBar, barParent).GetComponent<TrackMenuBar>().SetItem(item));
        }

        public void UpdateBar(int index)
        {
            bars[index].UpdateUI();
        }
    }
}