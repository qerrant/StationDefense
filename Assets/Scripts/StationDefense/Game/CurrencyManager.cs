using StationDefense.Patterns;
using UnityEngine.UI;

namespace StationDefense.Game
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        public int startCurrency = 250;
        public Text currencyText;
        private int currency;

        private void Start()
        {
            currency = startCurrency;
            currencyText.text = currency.ToString();
        }

        public int GetCurrency() => currency;

        public void AddCurrency(int currency)
        {
            this.currency += currency;
            currencyText.text = this.currency.ToString();
        }

        public void SubtractCurrency(int currency)
        {
            this.currency -= currency;
            currencyText.text = this.currency.ToString();
        }
    }
}
