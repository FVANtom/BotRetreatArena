using UnityEngine.UI;

namespace com.terranovita.botretreat
{
    public class HealthTagController : TagController, IBotDependant
    {
        public HealthTagController() : base(1.725f) { }

        public void UpdateBot(Bot bot)
        {
            var healthSlider = gameObject.GetComponentInChildren<Slider>();
            healthSlider.minValue = 0;
            healthSlider.maxValue = bot.PhysicalHealth.Maximum;
            healthSlider.value = bot.PhysicalHealth.Current;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}