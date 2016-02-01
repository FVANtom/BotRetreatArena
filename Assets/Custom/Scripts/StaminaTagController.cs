using UnityEngine;
using UnityEngine.UI;

namespace com.terranovita.botretreat
{
    public class StaminaTagController : MonoBehaviour, IBotDependant
    {

        public GameObject BotGameObject { get; set; }

        void Start()
        {

        }

        void Update()
        {
            // Position the name tag centered above the bot.
            var botPosition = BotGameObject.transform.position;
            transform.position = new Vector3(botPosition.x, 2.3f, botPosition.z);
            // Always let the name tags look directly at the camera.
            var mainCameraRotation = Camera.main.transform.rotation;
            transform.LookAt(transform.position + mainCameraRotation * Vector3.forward, mainCameraRotation * Vector3.up);
        }

        public void UpdateBot(Bot bot)
        {
            var staminaSlider = gameObject.GetComponentInChildren<Slider>();
            staminaSlider.minValue = 0;
            staminaSlider.maxValue = bot.Stamina.Maximum;
            staminaSlider.value = bot.Stamina.Current;
        }
    }
}