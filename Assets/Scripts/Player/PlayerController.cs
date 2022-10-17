using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 MovementInput;
    public float moveSpeed = 1.0f;
    public float maxHealth = 100;
    public float currentHealth;
    public float attackSpeed = 2.0f;
    public int money = 100;

    private float _collisionOffset = 0.02f;
    private bool _canMove = true;
    public SwordAttack swordAttack;

    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    public Collider2D col2d;
    public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        col2d = GetComponent<Collider2D>();

        swordAttack.attackCol.enabled = false;
        this.currentHealth = this.maxHealth;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_canMove)
        {
            if (MovementInput != Vector2.zero)
            {
                bool success = TryMove(MovementInput);
                if (!success)
                {
                    // Check if they can move left or right
                    success = TryMove(new Vector2(MovementInput.x, 0));

                    if (!success)
                    {
                        // Check if they can move up or down
                        success = TryMove(new Vector2(0, MovementInput.y));
                    }
                }
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }
        }
        // If movement input is not 0, move the character
        if(MovementInput.x < 0)
        {
            sr.flipX = true;
        }
        else if(MovementInput.x > 0)
        {
            sr.flipX = false;
        }
    }
    
    public bool TryMove(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + _collisionOffset);
        if(count == 0)
        {
            // Move the player
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void OnMove(InputValue Value)
    {
        // Get the input from Player Input component
        MovementInput = Value.Get<Vector2>();
    }

    private float _nextHit = 0.0f;
    private void OnFire()
    {
        if(Time.time >= _nextHit)
        {
            anim.SetTrigger("IsAttack");
            _nextHit = Time.time + attackSpeed;
        }
        
    }

    private IEnumerator CoroutineSkill()
    {
        attackSpeed = 0.3f;
        currentHealth += 50;
        coolDown = -10.0f;
        Debug.Log("Skill start!");
        yield return new WaitForSeconds(4);
        attackSpeed = 1.0f;
        StartCoroutine(CoroutineCooldown());
    }

    public float coolDown = 0.0f;
    public IEnumerator CoroutineCooldown()
    {
        while(coolDown < 0)
        {

            coolDown++;
            Debug.Log($"{coolDown + 10}");
            if(coolDown == 0)
            {
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnSkill()
    {
        if(coolDown == 0.0f)
        {
            StartCoroutine(CoroutineSkill());
        }
    }

    public void SwordAttack()
    {
        LockMove();
        if(sr.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndAttack()
    {
        CanMove();
        swordAttack.Stop();
    }

    public void CanMove()
    {
        _canMove = true;
    }
    public void LockMove()
    {
        // Block the player's movements
        _canMove = false;
    }

    private void Death()
    {
        LockMove();
        anim.SetBool("Death", true);
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }
}
