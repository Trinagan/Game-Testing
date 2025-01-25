using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;
    }
}
