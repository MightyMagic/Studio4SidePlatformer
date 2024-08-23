using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    [SerializeField] KeyCode turnLeft;
    [SerializeField] KeyCode turnRight;

    Rigidbody rb;
    Vector3 offset;
    float initialOffsetDIstance;

    bool left = false;
    bool right = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject tower = GameObject.Find("Tower");
        offset = new Vector3(rb.position.x - tower.transform.position.x, 0, rb.position.z - tower.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(turnLeft)) // && !isToucnhingVertical)
        {
            left= true;
        }
        else
        {
            left= false;
        }

        if (Input.GetKey(turnRight)) // && !isToucnhingVertical)
        {
            right= true;
        }
        else
        {
            right= false;
        }
    }

    void FixedUpdate()
    {
        if(left)
        {
            Rotate(RotationDirection.Left);
        }

        if(right)
        {
            Rotate(RotationDirection.Right);
        }
    }

    void Rotate(RotationDirection rotationDirection)
    {
        switch (rotationDirection)
        {
            case RotationDirection.Left:
                float rotationAmount = -rotationSpeed * Time.deltaTime;
               

                break;

            case RotationDirection.Right:
                rotationAmount = rotationSpeed * Time.deltaTime;

               

                break;
        }
    }

}
