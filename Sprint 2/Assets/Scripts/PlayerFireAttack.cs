using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireAttack : MonoBehaviour
{
    public SpriteRenderer sr;
    public Animator playerAnim;
    public float attackRate;            // time between attacks
    private float nextAttack;           // time of next attack
    private int damage = 10;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private bool attacking;
    private float timeAttackStart;

    private void Start()
    {
        attacking = false;
        this.sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (Time.time > nextAttack))
        {
            nextAttack = Time.time + attackRate;
            Attack();
            attacking = true;
            timeAttackStart = Time.time;
            this.sr.enabled = true;
        }
        if (this.sr.enabled && Time.time > timeAttackStart + 1f && attacking)
        {
            this.sr.enabled = false;
        }

    }

    void Attack()
    {
        this.playerAnim.SetTrigger("FireAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Mage mage = enemy.GetComponent<Mage>();
            Knight knight = enemy.GetComponent<Knight>();

            if (mage != null)
            {
                mage.TakeDamage(damage);
            }
            else if (knight != null)
            {
                knight.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
