namespace StationDefense.UI
{
    public class HealthBar : ProgressBar
    {
        public void SetHealth(float health)
        {
            SetProgress(health);
        }

        public void SetMaxHealth(float maxHealth)
        {
            SetMaxProgress(maxHealth);
        }
    }
}
