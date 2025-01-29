using UnityEngine;

public class ResourceOnDrop : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform player;

    public float scrapDrop;
    public float biomassDrop;
    public float weaponPartsDrop;
    private ResourceManager resourceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        resourceManager = GameObject.Find("Resource Manager").GetComponent<ResourceManager>();
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
            resourceManager.totalScrap += scrapDrop;
            resourceManager.totalBiomass += biomassDrop;
            resourceManager.totalWeaponParts += weaponPartsDrop;
            Destroy(gameObject);
        }
    }
}
