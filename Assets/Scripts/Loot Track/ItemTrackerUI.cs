using System.Collections.Generic;
using UnityEngine;

namespace LootTrack
{
    public class ItemTrackerUI : MonoBehaviour
    {
        static ItemTrackerUI instance;
        public static ItemTrackerUI Instance { get => instance; }

        [SerializeField] GameObject sampleBar;
        [SerializeField] Transform barParent;

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
            ItemTracker.Instance.Start();
            FetchBars();
        }

        public void Open()
        {
            gameObject.SetActive(true);
            if (bars.Count != ItemTracker.Instance.requiredItems.Count)
                FetchBars();
        }

        public void Close()
        {
            gameObject.SetActive(false);
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