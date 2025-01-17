using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed = 7;
    private float inputVertical;
    private float inputHorizontal;
    public float bulletSpeed = 1.5f;
    public float bulletLifetime = 1f;
    public float bulletOffsetDistance = 2;
    public float health = 100;
    private Vector3 mousePos;
    public GameObject bullet;
    
    void Start()
    {
        Cursor.visible = false;
        rig = GetComponent<Rigidbody2D>();
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

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        // Debug.Log(mousePos- transform.position);
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
        if (Input.GetMouseButtonDown(0))
        {
            // Create the mouse aim direction vector and normalize it
            Vector2 aimVector = transform.position - mousePos;
            Vector2 aimVectorNormalized = aimVector.normalized;
            // Offset the vector
            Vector2 bulletSpawnPos = (aimVectorNormalized * (bulletOffsetDistance * -1));
            GameObject bulletInstance = (GameObject) Instantiate(bullet, new Vector2(transform.position.x + bulletSpawnPos.x, transform.position.y + bulletSpawnPos.y), transform.rotation);
            Rigidbody2D bulletRig = bulletInstance.GetComponent<Rigidbody2D>();

            Vector2 aimPos = mousePos - transform.position;
            bulletRig.AddForce((bulletSpeed * 1000) * aimPos.normalized, ForceMode2D.Force);

           // Destroy(bulletInstance, bulletLifetime);
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 50;
        }
    }
}
