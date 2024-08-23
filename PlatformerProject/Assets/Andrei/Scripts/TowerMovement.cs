using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMovement : MonoBehaviour
{
    public List<GameObject> towerSegments;

    public float rotationSpeed;
    public bool alreadyRotating = false;

    [SerializeField] float minRotationSpeed;
    [SerializeField] float maxRotationSpeed;
    [SerializeField] float acceleration;

    [SerializeField] KeyCode turnLeft;
    [SerializeField] KeyCode turnRight;

    [SerializeField] float tiltThreshold;

    // public bool isToucnhingVertical = false;
    float leftThird;
    float rightThird;

    float leftHalf;
    float rightHalf;
    [SerializeField] float bottomFraction;


    void Start()
    {
        leftThird = Screen.width / 3f;
        rightThird = 2f * leftThird;

        leftHalf = Screen.width / 2f;
        //rightHalf = Screen.width / 2f;

        bottomFraction = Screen.height / bottomFraction;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKey(turnLeft)) // && !isToucnhingVertical)
        {
            alreadyRotating = true;
            Rotate(RotationDirection.Left);

            //if (!alreadyRotating)
            //{
            //    //rotationSpeed = minRotationSpeed;
            //    alreadyRotating = true;
            //}
        }
        
        
        if (Input.GetKey(turnRight)) // && !isToucnhingVertical)
        {
            alreadyRotating = true;
            Rotate(RotationDirection.Right);

            //if (!alreadyRotating)
            //{
            //    //rotationSpeed = minRotationSpeed;
            //    alreadyRotating = true;
            //}
        }   

#endif

#if UNITY_ANDROID

        // // Check if there are any touches
        // if (Input.touchCount > 0)
        // {
        //     // Get the first touch
        //     Touch touch = Input.GetTouch(0);
        //
        //     // Check if the touch position is on the left or right half of the screen
        //     if (touch.position.x < Screen.width / 2)
        //     {
        //         // Left half of the screen: Rotate left
        //         Rotate(RotationDirection.Left);
        //     }
        //     else
        //     {
        //         // Right half of the screen: Rotate right
        //         Rotate(RotationDirection.Right);
        //     }
        // }

        // Gyroscope ---------------------------------------------------------------------------------------------------

        //// Get the gyroscope attitude
        //Quaternion gyroAttitude = Input.gyro.attitude;
        //
        //// Extract the tilt angle (around the X-axis)
        //float tiltAngle = Mathf.Abs(gyroAttitude.eulerAngles.x);
        //
        //// Check if the tilt angle exceeds the threshold
        //if (tiltAngle > 0f) //tiltThreshold)
        //{
        //    // Determine the tilt direction (left or right)
        //    RotationDirection direction = (gyroAttitude.eulerAngles.x > 180f)
        //        ? RotationDirection.Left
        //        : RotationDirection.Right;
        //
        //    // Rotate based on the tilt direction
        //    Rotate(direction);
        //}

        // ------------------------------------------------------------------------------------------------------------
        if (Input.touchCount > 0)
        {

            if (!alreadyRotating)
            {
                //rotationSpeed = minRotationSpeed;
                alreadyRotating = true;
            }
               

            Touch touch = Input.GetTouch(0);

            // Check if the touch position is in the left, middle, or right third
            if (touch.position.x < leftHalf & touch.position.y < bottomFraction)
            {
                // Left third: Handle left touch
                Debug.Log("Left third touched!");
                Rotate(RotationDirection.Left);
            }
            else if (touch.position.x > leftHalf & touch.position.y < bottomFraction)
            {
                // Right third: Handle right touch
                Debug.Log("Right third touched!");
                Rotate(RotationDirection.Right);
            }
            else
            {
                // Middle third: Handle middle touch (tap)
                Debug.Log("Middle third tapped!");
            }

            if (Input.touchCount > 1)
            {
               Touch touch1 = Input.GetTouch(1);

                if (touch1.position.x < leftHalf & touch1.position.y < bottomFraction)
                {
                    // Left third: Handle left touch
                    Debug.Log("Left third touched!");
                    Rotate(RotationDirection.Left);
                }
                else if (touch1.position.x > leftHalf & touch1.position.y < bottomFraction)
                {
                    // Right third: Handle right touch
                    Debug.Log("Right third touched!");
                    Rotate(RotationDirection.Right);
                }
            }
        }
        else
        {
            if (alreadyRotating)
            {
                alreadyRotating = false;
                rotationSpeed = minRotationSpeed;
            }
        }

#endif
    }

    void Rotate(RotationDirection rotationDirection)
    {
        switch(rotationDirection)
        {
            case RotationDirection.Left:
                float rotationAmount = -rotationSpeed * Time.deltaTime;

                if (alreadyRotating & rotationSpeed <= maxRotationSpeed)
                {
                    rotationSpeed += acceleration * Time.deltaTime;
                }

                for (int i = 0; i < towerSegments.Count; i++)
                {
 
                   towerSegments[i].transform.Rotate(Vector3.up * rotationAmount);
                    
                }
                
                break;

            case RotationDirection.Right:

                rotationAmount = rotationSpeed * Time.deltaTime;

                if (alreadyRotating & rotationSpeed <= maxRotationSpeed)
                {
                    rotationSpeed += acceleration * Time.deltaTime;
                }

                for (int i = 0; i < towerSegments.Count; i++)
                {
                    towerSegments[i].transform.Rotate(Vector3.up * rotationAmount);      
                }

                break;
        }
    }
}

public enum RotationDirection
{
    Left, Right
}
