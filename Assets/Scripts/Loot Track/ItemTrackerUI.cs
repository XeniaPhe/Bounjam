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
        [SerializeField] GameObject background;

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
            Toggle();
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.I))
                Toggle();
        }

        private void Toggle()
        {
            if(background.gameObject.activeSelf)
            {
                background.gameObject.SetActive(false);
                ItemDetailsMenu.Instance.Unload();
            }
            else
                background.gameObject.SetActive(true);
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