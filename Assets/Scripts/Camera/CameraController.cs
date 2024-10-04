using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    float xVelocity = 0.0f;
    float yVelocity = 0.0f;
    float smoothTime = 0.3f;
    // How far the player can get away from camera
    float maxDistance = 2.0f;

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref xVelocity, smoothTime);
        float yPos = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref yVelocity, smoothTime);

        Vector2 cameraPos = BindCamera(xPos, yPos);

        transform.position = new Vector3(cameraPos.x, cameraPos.y, transform.position.z);

    }

    // Prevent player from getting too far away from camera
    Vector2 BindCamera(float xPos, float yPos) {
        if (xPos - player.transform.position.x > maxDistance) {
            xPos = player.transform.position.x + maxDistance;
        } else if (xPos - player.transform.position.x < -maxDistance) {
            xPos = player.transform.position.x - maxDistance;
        }
        if (yPos - player.transform.position.y > maxDistance) {
            yPos = player.transform.position.y + maxDistance;
        } else if (yPos - player.transform.position.y < -maxDistance) {
            yPos = player.transform.position.y - maxDistance;
        }
        return new Vector2(xPos, yPos);
    }
}
