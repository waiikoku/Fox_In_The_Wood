using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private PlayerSeeker _PlayerSeeker;
    [SerializeField] private Rigidbody2D _RB;
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private DestroyObject _DestroyObject;

    [SerializeField] private float _speed = 10;

    private bool alive = true;

    void Update()
    {
        if (_DestroyObject.destroy)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Animation();
        _RB.AddForce(_PlayerSeeker.playerPos * _speed * _PlayerSeeker.playerPos.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            alive = false;
        }
    }

    void Animation()
    {
        if (_RB.velocity.x > 0)
        {
            _sprite.flipX = true;
        }
        else if (_RB.velocity.x < 0)
        {
            _sprite.flipX = false;
        }
        if (!alive)
        {
            _anim.SetTrigger("destroy");
        }
    }

    public void TakeDamage()
    {
        alive = false;
    }
}
