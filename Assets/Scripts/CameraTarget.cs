using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public float cameraMagnitude = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 cameraPos = playerPos + (mousePos - playerPos) * cameraMagnitude;
        
        transform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
    }
}
