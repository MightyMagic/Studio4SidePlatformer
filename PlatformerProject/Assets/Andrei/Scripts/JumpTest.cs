using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float jumpHeight;
    [SerializeField] float jumpUpTime;
    [SerializeField] float jumpDownTime;

    public float g;
    public float gDown;
    public float v;
    float yVel = 0f;

    float timer = 0f;
    float fallTimer = 0f;

    public bool isGrounded = false;
    bool jumping = false;
    public bool falling = false;
    public bool justFalling = false;
    public bool startedFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        v = (2 * jumpHeight) / jumpUpTime;
        g = (-2 * jumpHeight) / (jumpUpTime * jumpUpTime);
        gDown = (-2 * jumpHeight) / (jumpDownTime* jumpDownTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            falling = false;
            justFalling= false;
            timer = 0f;
           // isGrounded = false;
        }
        if(!isGrounded)
        {

            if(timer < jumpUpTime && !jumping)
            {
                startedFalling = true;
                if (startedFalling)
                {
                    startedFalling = false;
                   // timer = 0f;
                    fallTimer = 0f;
                    //jumping = false;
                    falling = false;
                    justFalling = true;
                }
            }
            else if(timer > jumpDownTime)
            {
                startedFalling = true;
                if (startedFalling)
                {
                    startedFalling = false;
                    timer = 0f;
                    fallTimer = 0f;
                    //jumping = false;
                    falling = true;
                    justFalling = false;
                }

            }

            
           
        }
       // else if(!isGrounded && !jumping)
       // {
       //    // justFalling= true;
       //    //
       //    // jumping = false;
       //    // falling = false;
       // }
    }

    void FixedUpdate()
    {
        if(jumping)
        {
            yVel = v + g * timer;
            timer += Time.deltaTime;
        }
       
        if(falling)
        { 
            yVel = gDown * fallTimer;
            fallTimer += Time.deltaTime;
        }

        if (justFalling)
        {
            timer += Time.deltaTime;
            yVel = (-10f) * timer;
        }

        rb.velocity = new Vector3(0f, yVel, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        jumping = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
