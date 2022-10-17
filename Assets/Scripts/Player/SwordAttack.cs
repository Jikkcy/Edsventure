using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3.0f;

    public Collider2D attackCol;
    Vector2 rightAttackOffset;

    private void Start()
    {
        rightAttackOffset = transform.position;
        attackCol = this.GetComponent<Collider2D>();
    }

    public void AttackRight()
    {
        attackCol.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        attackCol.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y, 0);
    }

    public void Stop()
    {
        attackCol.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Slime enemy = other.GetComponent<Slime>();
            if(enemy != null)
            {
                enemy.Health -= damage;
                Debug.Log("Hit!");
            }
        }
    }
}
