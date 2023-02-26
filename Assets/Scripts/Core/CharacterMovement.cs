using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody => rb;
    private float velocityX;
    public float VelocityX
    {
        get
        {
            return velocityX;
        }
    }
    private float velocityMag;
    private bool isMoveable = true;
    private float directionX;
    private float speed;
    private readonly float inAirSpeed = 10f;

    private bool overrideGround = false;
    private bool grounded = false;
    public bool Grounded => grounded;
    private Collider2D tempCol;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundChkRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private bool jumpQueue = false;
    private float jumpHeight;
    private bool isJump = false;
    private bool inTheAir = false;
    private float jumpDelay = 1f;
    private float jumpTimestamp;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 newVelo = rb.velocity;
        velocityMag = newVelo.magnitude;
        newVelo.y = 0;
        velocityX = newVelo.magnitude;
    }

    private void FixedUpdate()
    {
        DoMove();
        GroundCheck();
        EngineBreak();
    }

    private void DoMove()
    {
        if (!isMoveable) return;
        if (directionX == 0) return;
        if (inTheAir)
        {
            rb.AddForce(new Vector2(directionX * inAirSpeed, 0));
        }
        else
        {
            rb.AddForce(new Vector2(directionX * speed, 0));
        }
        //rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
    }
    
    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void ShortFreeze()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(Freeze(1f));
    }

    private IEnumerator Freeze(float duration)
    {
        isMoveable = false;
        yield return new WaitForSeconds(duration);
        isMoveable = true;
    }

    public void OverrideGround(bool value)
    {
        overrideGround = value;
    }

    private void GroundCheck()
    {
        tempCol = Physics2D.OverlapCircle(groundChecker.position, groundChkRadius, groundLayer);
        grounded = tempCol == null ? false : true;

        //Clear Cache
        tempCol = null;

        inTheAir = !grounded;
    }

    private void EngineBreak()
    {
        if (inTheAir) return;
        if(directionX == 0)
        {
            rb.velocity *= 0.5f;
        }
        else
        {
            if(velocityMag > speed)
            {
                rb.velocity *= 0.9f;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    public void Move(float x)
    {
        directionX = x;
    }

    public void HandleJump(float jumpHeight)
    {
        jumpQueue = true;
        this.jumpHeight = jumpHeight;
        Jump();
    }

    public bool CanJump()
    {
        if (!jumpQueue) return false;
        if (inTheAir) return false;
        return true;
    }

    public void Jump()
    {
        if (!CanJump()) return;
        isJump = true;
        //rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        rb.AddForce(Vector2.up * jumpHeight);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundChecker.position, groundChkRadius);
    }
}
