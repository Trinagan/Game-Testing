using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    public Transform weaponOffset;
    public float speed = 7;
    private float inputVertical;
    private float inputHorizontal;
    public float bulletSpeed = 1.5f;
    public float bulletLifetime = 1f;
    public float bulletOffsetDistance = 2;
    public float health = 100;
    private Vector3 mousePos;
    public GameObject bullet;

    public PlayerHealthSystem playerHealthSystem;

    
    void Start()
    {
        playerHealthSystem = new PlayerHealthSystem(this); // Invoke the PHS and pass myself to it so it can call back
        Cursor.visible = false;
        rig = GetComponent<Rigidbody2D>();
        weaponOffset = GameObject.Find("Player/weaponOffset").GetComponent<Transform>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MoveH(inputVertical);
        MoveV(inputHorizontal);
        FollowMouse();
        ShootBullet();

        playerHealthSystem.CalculateStats();

        // This is no longer how we handle player health and death. PlayerHealthSystem will
        // manage it all and call back to this controller's OnPlayerDeath method when all limbs are deadge
        //if (health <= 0)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        
        // Debug.Log(mousePos- transform.position);
    }

    public void OnTakeDamage(int DamageAmount)
    {
        playerHealthSystem.TakeDamage(DamageAmount);
    }

    public void OnPlayerDeath()
    {
        // Trigger animations, fiddle with whatever stats you need, change gamestate etc etc
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MoveH(float move)
    {
        rig.linearVelocity = new Vector2(move * speed, rig.linearVelocity.x);
    }

    public void MoveV(float move)
    {
        rig.linearVelocity = new Vector2(move * speed, rig.linearVelocity.x);
    }

    public void FollowMouse()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }

    public void ShootBullet()
    {
        // Rewrite in the future to call the Weapon framework to shoot a projectile.
        if (Input.GetMouseButtonDown(0))
        {
            // Create the mouse aim direction vector and normalize it
            Vector2 aimVector = transform.position - mousePos;
            Vector2 aimVectorNormalized = aimVector.normalized;

            GameObject bulletInstance = (GameObject)Instantiate(bullet, weaponOffset.transform.position, transform.rotation);
            Rigidbody2D bulletRig = bulletInstance.GetComponent<Rigidbody2D>();

            Vector2 aimPos = mousePos - transform.position;
            bulletRig.linearVelocity = aimPos.normalized * bulletSpeed;

            // Destroy(bulletInstance, bulletLifetime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && collision != null)
        {
            // This will need to be a bit smarter in future.
            // Probably pre-calc the total raw damage in the enemy controller
            // which'd be a convenient way to factor in enemy stats etc etc before
            // applying mitigation in the PHS
            OnTakeDamage(50); 
        }
    }
}
