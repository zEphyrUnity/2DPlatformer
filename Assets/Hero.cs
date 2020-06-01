using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed = 1.2F;
    private float _jumpHeight = 3;

    private sbyte _direction;
    private bool _dead, _onFloor;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Camera _mc;
    private Animator HeroAnimator;
    void Start()
    {
        _dead = false;
        _mc = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        HeroAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _mc.transform.position = new Vector3(transform.position.x, transform.position.y, _mc.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _sr.flipX = false;
            _direction = 1;
            Move();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _sr.flipX = true;
            _direction = -1;
            Move();
        }
        else
        {
            Stop();
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && _onFloor)
        {
            _onFloor = false;
            Jump();
        }

        if (_rb.velocity.y == 0)
        {
            _onFloor = true;
        }
    }

    private void Move()
    {
        if (_rb.velocity.y > 0)
        {
            HeroAnimator.SetBool("bMove", false);
            HeroAnimator.SetBool("bJump", true);
        }

        if (_rb.velocity.y == 0)
        {
            HeroAnimator.SetBool("bMove", true);
            HeroAnimator.SetBool("bJump", false);
        }

        _rb.velocity = new Vector2(_speed * _direction, _rb.velocity.y);
    }

    private void Stop()
    {
        HeroAnimator.SetBool("bJump", false);
        HeroAnimator.SetBool("bMove", false);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    private void Jump()
    {
        HeroAnimator.SetBool("bMove", false);
        HeroAnimator.SetBool("bJump", true);
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
    }
}
