using UnityEngine;

namespace com.terranovita.botretreat
{
    public class NameTagController : TagController, IBotDependant
    {
        public NameTagController() : base(2.0f) { }

        public void UpdateBot(Bot bot)
        {
            var txtMesh = gameObject.GetComponent<TextMesh>();
            txtMesh.text = bot.Name;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}