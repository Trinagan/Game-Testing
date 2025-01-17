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
        
        Debug.Log(mousePos- transform.position);
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
            GameObject bulletInstance = (GameObject) Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D bulletRig = bulletInstance.GetComponent<Rigidbody2D>();

            Vector2 aimPos = mousePos - transform.position;
            bulletRig.AddForce((bulletSpeed * 1000) * aimPos.normalized, ForceMode2D.Force);

        }
    }
}
