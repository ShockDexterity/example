using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D physics;
    Vector2 speed;
    public Vector2 jump;
    public bool jumping;
    float jumpY;

    public int health = 10;

    public Animator anim;
    public bool idle;
    public bool facingLeft;
    public bool blocking;

    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        speed = new Vector2(2.5f, 0);
        jump = new Vector2(0, 10f);

        idle = true;
        //physics = this.GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(10, 12);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y<-5)
        {
            TakeDamage(1);
        }

        

        if (jumping != true)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                //user is pressing key this frame
                physics.AddForce(jump, ForceMode2D.Impulse);
                // Debug.Log("Pressing Key");
                //physicsEngine.velocity = new Vector2(0, 10);
                jumping = true;
                this.idle = false;
                anim.SetBool("isIdle", this.idle);
                anim.SetBool("isJumping", jumping);
            }
            //else 
            else
            {
                //
                //Debug.Log("Not pressing Key");
            }
        }
        else
        {
            //user has jumped
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (!jumping)
            {
                this.idle = false;
                anim.SetBool("isIdle", this.idle);
            }
            this.transform.localScale = Vector3.one;
            this.facingLeft = false;
            physics.velocity = speed + new Vector2(0, physics.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!jumping)
            {
                this.idle = false;
                anim.SetBool("isIdle", this.idle);
            }
            this.transform.localScale = new Vector3(-1, 1, 1);
            this.facingLeft = true;
            physics.velocity = -speed + new Vector2(0, physics.velocity.y);
        }
        else
        {
            this.idle = true;
            anim.SetBool("isIdle", this.idle);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("NoEnemy"))
        {
            //nothing happens. jump is stil disabled
            jumping = false;
            anim.SetBool("isJumping", jumping);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!blocking)
        {
            health -= damage;
        }

        if(health<1)
        {
            Destroy(this.gameObject);
        }
    }
}
