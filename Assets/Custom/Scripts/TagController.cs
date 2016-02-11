using System;
using UnityEngine;

namespace com.terranovita.botretreat
{
    public class TagController : MonoBehaviour
    {
        public GameObject BotGameObject { get; set; }

        private readonly Single _offset;

        public TagController(Single offset)
        {
            _offset = offset;
        }

        private void Update()
        {
            // Position the name tag centered above the bot.
            var botPosition = BotGameObject.transform.position;
            transform.position = new Vector3(botPosition.x, _offset, botPosition.z);
            // Always let the name tags look directly at the camera.
            var mainCameraRotation = Camera.main.transform.rotation;
            transform.LookAt(transform.position + mainCameraRotation * Vector3.forward, mainCameraRotation * Vector3.up);
        }
    }
}