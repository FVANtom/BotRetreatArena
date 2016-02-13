using UnityEngine.UI;

namespace com.terranovita.botretreat
{
    public class StaminaTagController : TagController, IBotDependant
    {
        public StaminaTagController() : base(1.8f) { }

        public void UpdateBot(Bot bot)
        {
            var staminaSlider = gameObject.GetComponentInChildren<Slider>();
            staminaSlider.minValue = 0;
            if(bot.Stamina != null) {
                staminaSlider.maxValue = bot.Stamina.Maximum;
                staminaSlider.value = bot.Stamina.Current;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}