using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed = 7;
    private float inputVertical;
    private float inputHorizontal;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        MoveH(inputVertical);
        MoveV(inputHorizontal);
        followMouse();
        
    }

    public void MoveH(float move)
    {
        rig.linearVelocity = new Vector2(move * speed, rig.linearVelocity.x);
    }

    public void MoveV(float move)
    {
        rig.linearVelocity = new Vector2(move * speed, rig.linearVelocity.x);
    }

    public void followMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
