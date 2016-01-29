using UnityEngine;

public class NameTagController : MonoBehaviour
{
    private const float NAME_TAG_SCALE = 0.015f;

    public GameObject BotGameObject { get; set; }

    void Start()
    {
        Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(oldScale.x * NAME_TAG_SCALE, oldScale.y * NAME_TAG_SCALE, oldScale.z);
    }

    void Update()
    {
        // Position the name tag centered above the bot.
        var botPosition = BotGameObject.transform.position;
        transform.position = new Vector3(botPosition.x, 2, botPosition.z);
        // Always let the name tags look directly at the camera.
        var mainCameraRotation = Camera.main.transform.rotation;
        transform.LookAt(transform.position + mainCameraRotation * Vector3.forward, mainCameraRotation * Vector3.up);
    }
}