using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] GameObject lightSource;


    [SerializeField] float rotationSpeedY;
    [SerializeField] float maxAngle;

    int startDirection = 1;

    void Start()
    {
        //RandomizeDirection();
    }

    void Update()
    {

        lightSource.transform.rotation *= Quaternion.Euler(0f, startDirection * rotationSpeedY * Time.deltaTime, 0f);
        //if(Mathf.Abs(lightSource.transform.rotation.eulerAngles.y) < maxAngle)
        //{
        //    lightSource.transform.rotation *= Quaternion.Euler(0f, startDirection * rotationSpeedY * Time.deltaTime, 0f);
        //}
        //else
        //{
        //     Debug.Log("Angle is " + lightSource.transform.rotation.eulerAngles.y);
        //     lightSource.transform.rotation = Quaternion.Euler(0f, startDirection * maxAngle - (startDirection * 2f), 0f);
        //     ChangeDirection(); 
        //}

        //lightSource.transform.rotation *= Quaternion.Euler(0f, startDirection * rotationSpeedY * Time.deltaTime, 0f);

    }

    void ChangeDirection()
    {
        startDirection *= (-1);
    }
}
