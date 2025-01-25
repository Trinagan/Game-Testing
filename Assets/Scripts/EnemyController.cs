using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public GameObject detector;
    private CircleCollider2D col;
    public float health = 100;
    private bool playerDetected = false;
    private bool followPlayer = false;
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    private Vector2 originalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = detector.GetComponentInChildren<CircleCollider2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FaceDirection(Vector3 targ)
    {
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), 200f * Time.deltaTime);
    }

    void FollowPlayer()
    {
        int layerMasks = playerLayer | wallLayer;
        Vector2 playerDirection = target.transform.position - transform.position;
        playerDirection.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, 10, layerMasks);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            followPlayer = true;
        }

        if (playerDetected && followPlayer)
        {
            agent.SetDestination(target.position);
            FaceDirection(target.transform.position);
        } else if (!playerDetected)
        {
            agent.SetDestination(originalPos);
            FaceDirection(originalPos);
            followPlayer = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 30;
        }
    }
}
