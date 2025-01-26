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
    private float fleeDistance = 15f;
    private int fleeAngleSteps = 12;
    private float stoppingDistance;
    private bool playerDetected = false;
    private bool followPlayer = false;
    private bool playerInSight = false;
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    private int layerMasks;
    private Vector2 originalPos;
    private Vector2 lastPos;

    public enum EnemyTypes
    {
        SoliderAI,
        ScientistAI,
        AlienAI
    };

    public EnemyTypes enemyType = EnemyTypes.SoliderAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMasks = playerLayer | wallLayer;
        target = GameObject.Find("Player").GetComponent<Transform>();
        col = detector.GetComponentInChildren<CircleCollider2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (enemyType.ToString() == "SoliderAI")
        {
            originalPos = transform.position;
            stoppingDistance = Random.Range(5.0f, 8.0f);
            agent.stoppingDistance = stoppingDistance;
        }

        if (enemyType.ToString() == "ScientistAI")
        {
            agent.stoppingDistance = 15;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType.ToString() == "SoliderAI")
        {
            FollowPlayer();
        }

        if (enemyType.ToString() == "ScientistAI")
        {
            RunAwayFromPlayer();
        }
        
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
        OnRaycastHit2D();
        if (playerDetected && followPlayer)
        {
            agent.SetDestination(target.transform.position);
            FaceDirection(lastPos);
        }

        if (playerDetected && agent.stoppingDistance >= agent.remainingDistance)
        {
            if (agent.stoppingDistance > 1.0f && !playerInSight)
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

    void RunAwayFromPlayer()
    {
        OnRaycastHit2D();
        if(playerDetected && playerInSight)
        {
            Vector2 oppositePlayerDirection = transform.position - target.transform.position;
            oppositePlayerDirection.Normalize();
            Vector2 bestFleePosition = (Vector2)transform.position;
            float maxDistanceFromPlayer = 0;

            for (int i = 0; i < fleeAngleSteps; i++)
            {
                float angle = 360f / fleeAngleSteps * i;
                Vector2 rotatedDirection = Quaternion.Euler(0, angle, 0) * oppositePlayerDirection;
                Vector2 potentialFleePosition = (Vector2)transform.position + rotatedDirection * fleeDistance;

            if (NavMesh.SamplePosition(potentialFleePosition, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
            {
                float distanceToPlayer = Vector2.Distance(hit.position, target.position);

                if (distanceToPlayer > maxDistanceFromPlayer)
                {
                    maxDistanceFromPlayer = distanceToPlayer;
                    bestFleePosition = hit.position;
                }
            }
            /*             RaycastHit2D hit = Physics2D.Raycast(transform.position, oppositePlayerDirection, 15);
                        Debug.DrawRay(transform.position, oppositePlayerDirection);
                        if(hit.distance < 15)
                        {
                            Vector3 v3 = (Vector3)oppositePlayerDirection;
                            Quaternion rotation = Quaternion.Euler(0, 10, 0);
                            v3 = rotation * v3;
                            oppositePlayerDirection = (Vector2)v3;
                        }
                        else
                        {
                            agent.SetDestination(oppositePlayerDirection);
                        } */
            }
            agent.SetDestination(bestFleePosition);
            FaceDirection(bestFleePosition);
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

    void OnRaycastHit2D()
    {
        Vector2 playerDirection = target.transform.position - transform.position;
        playerDirection.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, 10, layerMasks);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            followPlayer = true;
            lastPos = target.transform.position;
            playerInSight = true;
        }
        else
        {
            playerInSight = false;
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
