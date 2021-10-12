using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 0f;
    public int damage;

    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    public Vector3 initialPos;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        if (Mathf.Abs(transform.position.x) > 15 + Mathf.Abs(initialPos.x))
        {
            Destroy(this.gameObject);
            Debug.Log(this.gameObject.name + " went out of bounds");
        }
    }

    public void Direction(bool dir)
    {
        moveSpeed = (dir) ? 3f : -3f;
        if(dir)
        {
            this.transform.localScale = Vector3.one;
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "NoEnemy":
                //Debug.Log("Fireball passed through " + collision.gameObject.name);
                break;

            case "ProjectileAllowed":
                //Debug.Log("Fireball passed through " + collision.gameObject.name);
                break;

            case "Mage":
                //Debug.Log("Fireball passed through " + collision.gameObject.name);
                break;

            case "Knight":
                //Debug.Log("Fireball passed through " + collision.gameObject.name);
                break;

            case "PlayerTag":
                GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>().TakeDamage(damage);
                Destroy(this.gameObject);
                break;

            default:
                //Debug.Log("Fireball hit " + collision.gameObject.name);
                Destroy(this.gameObject);
                break;
        }
    }
}