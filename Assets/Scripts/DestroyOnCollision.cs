using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
