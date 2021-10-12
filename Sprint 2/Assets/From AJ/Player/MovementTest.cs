using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D physicsEngine;
    Vector2 firstVector;
    Vector2 rightVector;
    Vector2 leftVector;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        firstVector = new Vector2(0, 5);
        rightVector = new Vector2(5, 0);
        leftVector = new Vector2(-5, 0);
        physicsEngine = GameObject.Find("MC_SwordAttack_Animation_SpriteSheet 1_0").GetComponent<Rigidbody2D>();
        //physicsEngine.AddForce(firstVector, ForceMode2D.Impulse);
        Debug.Log("yaaay. script is working");
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if(Input.GetKey(KeyCode.UpArrow))
        {
            //user is pressing key this frame
            physicsEngine.AddForce(firstVector);
            Debug.Log("Pressing Key");
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            physicsEngine.AddForce(rightVector);
            Debug.Log("Pressing Key");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            physicsEngine.AddForce(leftVector);
            Debug.Log("Pressing Key");
        }
        else
        {
            //
            Debug.Log("Not pressing Key");
        }

    */

        if (isJumping != true)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //user is pressing key this frame
                physicsEngine.AddForce(firstVector, ForceMode2D.Impulse);
                Debug.Log("Pressing Key");
                isJumping = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                physicsEngine.AddForce(rightVector);
                Debug.Log("Pressing Key");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                physicsEngine.AddForce(leftVector);
                Debug.Log("Pressing Key");
            }
            else
            {
                //
                Debug.Log("Not pressing Key");
            }
        }
        else
        {
            //user has jumped
        }

        if (physicsEngine.velocity.y == 0)
        {

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //runs on collision
        if (collision.gameObject.name == "pacmanWall")
        {
            //nothing happens. jump is stil disabled
        }
        else
        {
            Debug.Log("Collision Detected");
            isJumping = false;
        }

    }
}