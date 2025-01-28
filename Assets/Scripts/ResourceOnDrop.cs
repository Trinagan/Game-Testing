using UnityEngine;

public class ResourceOnDrop : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform player;

    private float scrapDrop;
    private float biomassDrop;
    private float weaponPartsDrop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 vacuumToPlayer = player.transform.position - transform.position;
            rig.linearVelocity = vacuumToPlayer * 5;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
