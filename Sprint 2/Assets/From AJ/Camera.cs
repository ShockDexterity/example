using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform lookAt;

    // Start is called before the first frame update
    void Start()
    {
        /*eop*/
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        transform.position += new Vector3(deltaX, deltaY, 0);
    }
}

