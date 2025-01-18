using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed = 7;
    private float inputVertical;
    private float inputHorizontal;
    public float bulletSpeed = 1.5f;
    public float bulletLifetime = 1f;
    public float bulletOffsetDistance = 2;
    private Vector3 mousePos;
    public GameObject bullet;
    
    void Start()
    {
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
            Vector3 aimVector = transform.position - mousePos;
            Vector3 aimVectorNormalized = aimVector.normalized;
            // Offset the vector
            Vector3 bulletSpawnPos = (aimVectorNormalized * (bulletOffsetDistance * -1));
            GameObject bulletInstance = (GameObject) Instantiate(bullet, new Vector3(transform.position.x + bulletSpawnPos.x, transform.position.y + bulletSpawnPos.y, 1), transform.rotation);
            Rigidbody2D bulletRig = bulletInstance.GetComponent<Rigidbody2D>();

            Vector2 aimPos = mousePos - transform.position;
            bulletRig.AddForce((bulletSpeed * 1000) * aimPos.normalized, ForceMode2D.Force);

           // Destroy(bulletInstance, bulletLifetime);
        }
    }
}
