using StationDefense.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StationDefense.UI
{
    public class BuildingButton : MonoBehaviour
    {
        public Image icon;
        public Text turretName;
        public Text description;
        public Text cost;
        public GameObject prefab;
        public UnityEvent OnButtonClicked;
        private int _price = 0;

        private void Start()
        {
            if (OnButtonClicked == null)
            {
                OnButtonClicked = new UnityEvent();
            }
        }

        public void SetActiveButton(bool isActive)
        {
            Button btn = GetComponent<Button>();
            if (btn == null) return;
            btn.interactable = isActive;
        }
           
        private void Update()
        {
            if (!CurrencyManager.instanceExists) return;
            if (_price > CurrencyManager.instance.GetCurrency())
            {
                SetActiveButton(false);
            }
            else
            {
                SetActiveButton(true);
            }
        }
               
        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        public void SetName(string name)
        {
            turretName.text = name;
        }

        public void SetDescription(string description)
        {
            this.description.text = description;
        }

        public void SetPrice(int cost)
        {
            this.cost.text = cost.ToString();            
            _price = cost;
        }

        public void OnClick()
        {       
            CurrencyManager.instance.SubtractCurrency(_price);
            OnButtonClicked?.Invoke();
        }
    }
}
