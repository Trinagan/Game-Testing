using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Rigidbody2D rig;

    // Implementing a base stat (which can be altered by factors) -> Max stat -> current stat system
    // this way we're set up for possible things like healing (but capping at max hp) etc
    public float BaseMaxHealth = 100f;
    public float MaxHealth;
    public float CurrentHealth;
    
    // Get this stuff done ASAP on load, probably would be fine in Start but I'm paranoid
    void Awake()
    {
        // Init stats based on other factors
        InitialiseStats();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // We will want to replace this static amount with a reference to the attacking projectile's stats
            // e.g. Bullet might do x damage, FlameThrowerTick might be 15, RPG = 50 etc
            CurrentHealth -= 30;
        }
    }

    void InitialiseStats()
    {
        // Unused stub for now.
        // Possible implementation:
        // Scale CurrentHealth based on additional factors such as level, game difficulty or enemy variant e.g.
        // Enemy hp scales 10% (0.1) per level, +/- 30% (0.3) based on easy/normal/hard difficulty and MeanVariant adds 25% (0.25)
        // A MeanVariant on level 3 on the easy difficulty would be
        // MaxHealth = BaseMaxHealth * (1 + ([LevelModifier] + [VariantModifier]) * (1 + [DifficultyModifier]))
        // MaxHealth = 100 * 1.55 * 0.7 = 108.5

        MaxHealth = BaseMaxHealth;
        CurrentHealth = MaxHealth;
    }

    // Moved this into it's own function to give us room to handle everything that should happen in a neat way
    // e.g. trigger animations, spawn dropped loot, trigger events etc
    void OnDeath()
    {
        Destroy(gameObject);
    }
}
