 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float Health
    {
        set
        {
            _health = value;

            if(_health <= 0)
            {
                anim.SetBool("Death", true);
            }
        }
        get
        {
            return _health;
        }
    }

    private float _health = 5.0f;
    private float _damage = 10.0f;
    private float _attackSpeed = 1.0f;
    private float _hitLast = 0.0f;
    private float _movementSpeed = 0.3f;

    private Vector2 _movement;
    private Transform _target;
    public Animator anim;
    public Transform player;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = _target.position - this.transform.position;
        _movement = position;
    }

    private void FixedUpdate()
    {
        Move(_movement);
    }

    public void Defeated()
    {
        Destroy(gameObject);
    }

    public void Move(Vector2 position)
    {
        anim.SetBool("IsMoving", true);
        if(_movement.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        rb.MovePosition((Vector2)transform.position + (position.normalized * this._movementSpeed * Time.fixedDeltaTime)); 
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(Time.time - _hitLast < _attackSpeed)
            {
                return;
            }

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.currentHealth -= this._damage;
            }
            _hitLast = Time.time;
        }
    }
}
