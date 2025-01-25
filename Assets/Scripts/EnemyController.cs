using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public GameObject detector;
    public Rigidbody2D bullet;
    private CircleCollider2D col;
    public float health = 100;
    private float stoppingDistance;
    private bool playerDetected = false;
    private bool followPlayer = false;
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    private Vector2 originalPos;
    private Vector2 lastPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        col = detector.GetComponentInChildren<CircleCollider2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originalPos = transform.position;
        stoppingDistance = Random.Range(5.0f, 8.0f);
        agent.stoppingDistance = stoppingDistance;

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        
        /* if (playerDetected && followPlayer && agent.remainingDistance <= agent.stoppingDistance)
        {
            InvokeRepeating(nameof(ShootPlayer), 0, 5);
        }
 */
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
            lastPos = target.transform.position;
        }

        if (playerDetected && followPlayer)
        {
            agent.SetDestination(target.transform.position);
            FaceDirection(lastPos);
        }

        if (playerDetected && agent.stoppingDistance >= agent.remainingDistance)
        {
            if (agent.stoppingDistance > 1.0f && !hit.collider.CompareTag("Player"))
            {
                agent.stoppingDistance--;
            }
            else
            {
                agent.stoppingDistance = stoppingDistance;
            }
        }

        if (!playerDetected)
        {
            agent.SetDestination(originalPos);
            FaceDirection(originalPos);
            followPlayer = false;
        }
    }

    void ShootPlayer()
    {
        Vector2 aimVector = transform.position - target.transform.position;
        Vector2 aimVectorNormalized = aimVector.normalized;

        Vector2 bulletSpawnPos = aimVectorNormalized * (2 * -1);
        Rigidbody2D bulletInstance = Instantiate(bullet, new Vector2(transform.position.x + bulletSpawnPos.x, transform.position.y + bulletSpawnPos.y), transform.rotation);

        Vector2 aimPos = target.transform.position - transform.position;
        bulletInstance.AddForce((1.5f * 1000) * aimPos.normalized, ForceMode2D.Force);
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
