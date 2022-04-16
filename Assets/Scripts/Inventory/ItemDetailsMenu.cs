using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace Inventory
{
    public class ItemDetailsMenu : MonoBehaviour
    {
        static ItemDetailsMenu instance;
        public static ItemDetailsMenu Instance { get => instance; }

        [SerializeField] TMP_Text itemNameText;
        [SerializeField] TMP_Text descriptionText;
        [SerializeField] TMP_Text statusText;
        [SerializeField] Image itemIcon;

        private void Awake()
        {
            if (instance)
                Destroy(this);
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            Unload();
        }

        public void Unload()
        {
            gameObject.SetActive(false);
        }

        public void Load(Item item,Vector2 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            itemNameText.text = item.Name;
            descriptionText.text = item.Description;
            statusText.text = "Status : " + (item.isAcquired ? "Acquired" : "Unacquired");
            itemIcon.sprite = item.Icon;
        }
    }
}
