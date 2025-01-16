using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;

    public float JumpForce = 8;
    public float speed = 7;
    private float inputVertical;
    private float inputHorizontal;
    public float groundedThreshold = 0.8f;

    public LayerMask GroundLayer;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        MoveH(inputHorizontal);
        MoveV(inputVertical);
    }

    public void MoveH(float move)
    {
        rig.linearVelocity = new Vector2(move * speed, rig.linearVelocity.y);
    }

    public void MoveV(float move)
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rig.linearVelocity = Vector2.up * JumpForce;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedThreshold, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
