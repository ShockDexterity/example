using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public GameObject player;               // the player
    private float playerX;                  // player x coord
    private float knightX;                  // knight x coord
    private float playerY;                  // player y coord
    private float knightY;                  // knight y coord

    private Rigidbody2D physics;            // holds the knight's rigidbody for movement
    public bool facingLeft = false;         // is the mage facing left? based on initial spritesheet
    public int health = 8;                  // health of the knight

    public float attackRate;                // time between attacks
    private float nextAttack;               // time of next attack
    private int damage = 5;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    private Vector2 speed;                  // how fast the mage can go
    private float moveRate = 1f;            // how long the mage will move
    private float moveCounter = 0f;         // how long the mage has moved
    public int dirX;                        // direction of movement
    public bool seesPlayer = false;         // did the mage see the player?

    // Animation booleans
    public Animator anim;
    public bool idle;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 11);

        this.idle = true;
        this.damage = 2;

        // grabbing the rigidbody component for movement
        this.physics = this.gameObject.GetComponent<Rigidbody2D>();
        this.speed = new Vector2(1.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.seesPlayer)
        {
            if (this.moveCounter > this.moveRate)
            {
                this.ChangeDirection();
                this.moveCounter = 0f;
            }

            switch (this.dirX)
            {
                case 1:
                    this.idle = false;
                    anim.SetBool("isIdle", this.idle);
                    this.transform.localScale = Vector3.one;
                    this.facingLeft = false;
                    break;

                case -1:
                    this.idle = false;
                    anim.SetBool("isIdle", this.idle);
                    this.transform.localScale = new Vector3(-1, 1, 1);
                    this.facingLeft = true;
                    break;

                case 0:
                    this.idle = true;
                    anim.SetBool("isIdle", this.idle);
                    break;
            }

            if (Mathf.Abs(this.physics.velocity.x) < speed.x)
            {
                this.physics.AddForce(speed * dirX);
            }
            else
            {
                this.physics.velocity = speed * dirX;
            }

            moveCounter += Time.deltaTime;

            FindPlayer();
        }
        else
        {
            playerX = player.transform.position.x;
            knightX = this.transform.position.x;

            if (playerX > knightX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
                dirX = 1;
                this.physics.velocity = speed * dirX;
                anim.SetBool("isIdle", false);
            }
            else if (playerX < knightX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
                dirX = -1;
                this.physics.velocity = speed * dirX;
                anim.SetBool("isIdle", false);
            }

            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                Attack();
            }
        }
    }

    // Randomly generates the direction of movement
    private void ChangeDirection()
    {
        this.dirX = Random.Range(-1, 2);
    }

    // Looks for the player
    private void FindPlayer()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        knightX = this.transform.position.x;
        knightY = this.transform.position.y;

        if (Mathf.Abs(playerY - knightY) < 2)
        {
            if (playerX > knightX && !this.facingLeft)
            {
                if (playerX - knightX <= 5f)
                {
                    seesPlayer = true;
                    //Debug.Log("I SAW THE PLAYER");
                    return;
                }
            }
            else if (playerX < knightX && facingLeft)
            {
                if (Mathf.Abs(playerX - knightX) <= 5f)
                {
                    seesPlayer = true;
                    //Debug.Log("I SAW THE PLAYER");
                    return;
                }
            }
        }
    }

    // Allows the knight to take damage and die
    public void TakeDamage(int damage)
    {
        this.health -= damage;

        if (this.health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Attack()
    {
        this.anim.SetTrigger("isAttacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitEnemies)
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}