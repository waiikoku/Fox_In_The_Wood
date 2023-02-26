using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerSeeker _PlayerSeeker;
    [SerializeField] private Rigidbody2D _RB;
    [SerializeField] private GameObject _GFX;
    [SerializeField] private DestroyObject _DestroyObject;

    [SerializeField] private float _cooldown = 5;
    [SerializeField] private float _speed = 1;

    private float _timer = 0;
    private bool touchingGround = false;
    private bool alive = true;

    private void Start()
    {
        touchingGround = true;
    }

    void Update()
    {
        if (_timer >= _cooldown)
        {
            Jump();
        }
        if (_DestroyObject.destroy)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_PlayerSeeker.playerPos != Vector2.zero && touchingGround)
        {
            _timer += Time.deltaTime;
        }

        Animation();
    }

    void Jump()
    {
        Vector2 jump = CalculateProjectileVelocity(_RB.position, _PlayerSeeker.playerPos, _speed);
        _timer = 0;
        _RB.velocity = jump;
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance2D = target - origin;
        Vector2 distance = distance2D;
        distance.y = 0;

        float distX = distance.magnitude;
        float distY = distance2D.y;

        float velocityX = distX / time;
        float velocityY = distY / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = distance.normalized;
        result *= velocityX;
        result.y = velocityY;
        return result;
    }

    void Animation()
    {
        Animator anim = _GFX.GetComponent<Animator>();
        SpriteRenderer sprite = _GFX.GetComponent<SpriteRenderer>();

        if (_RB.velocity.x > 0)
        {
            sprite.flipX = true;
        }
        else if (_RB.velocity.x < 0)
        {
            sprite.flipX = false;
        }

        anim.SetBool("grounded", touchingGround);
        anim.SetInteger("velocityY", (int)_RB.velocity.y);

        if(!alive)
        {
            anim.SetTrigger("destroy");
        }
    }

    public void TakeDamage()
    {
        alive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter c = collision.gameObject.GetComponent<PlayerCharacter>();
            if(c != null)
            {
                c.TakeDamage(1);
            }
        }
    }
}
