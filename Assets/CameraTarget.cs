using Unity.Cinemachine;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = 5.0f; 
        float maxDistance = 15.0f;
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector2 direction = (mousePos - playerPos).normalized;
        float distance = Vector2.Distance(playerPos, mousePos);

        float clampedDistance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector2 cameraPos = playerPos + direction * clampedDistance;
        
        transform.position = cameraPos;
    }
}
