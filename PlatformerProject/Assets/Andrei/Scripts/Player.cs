using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlatformType playerType;
    public PlayerState playerState;
    GameManager gameManager;

    [SerializeField] float jumpMagnitude;
    [SerializeField] float gravityMultiplier;

    [SerializeField] ParticleSystem red;
    [SerializeField] ParticleSystem blue;


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image textBackground;
    Rigidbody rb;

    [Header("Audio")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip colorSwap;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource1;

    float leftThird;
    float rightThird;
    float leftHalf;
    float rightRight;

    //float bottomThird;
    [SerializeField] float bottomFraction;

    public bool canJump = false;

    public PlayerState PlayerStateField => playerState;



    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();

        Vector3 grav = new Vector3(0f, -9.8f, 0f) * gravityMultiplier;  
        Physics.gravity = grav;

        Input.gyro.enabled = true;

        switch (playerType)
        {
            case PlatformType.Blue:
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[0];
                gameObject.layer = 7;
                //scoreText.color = gameManager.platformMaterials[0].color;
                //textBackground.color = gameManager.platformMaterials[1].color;
                break;
            case PlatformType.Red:
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[1];
                gameObject.layer = 6;
                //scoreText.color = gameManager.platformMaterials[1].color;
                //textBackground.color = gameManager.platformMaterials[0].color;
                break;
        }


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
        //Calls the method for changing the color
        if (Input.GetKeyDown(KeyCode.Mouse0)) // && playerState == PlayerState.OutsidePlatform)
        {
            ChangeColor();
        }

        // Simple jump for testing purposes
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump= false;
            Jump();
           
        }

#endif

#if UNITY_ANDROID

        //if(Input.touchCount > 1)
        //{
        //    ChangeColor();
        //}

        if(Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            
            if(Input.touchCount > 1)
            {
                Touch touch1 = Input.GetTouch(1);

                if (touch1.phase == TouchPhase.Began & touch1.position.y > bottomFraction) 
                {
                    ChangeColor();
                }
            }

            if (touch.phase == TouchPhase.Began & touch.position.y > bottomFraction) //& (touch.position.x < (Screen.width * 2f / 3f)) & (touch.position.x > (Screen.width / 3f)))
            {
                ChangeColor();
                //if (canJump)
                //{
                //    canJump = false;
                //    Jump();
                //}
                //else
                //{
                //    ChangeColor();
                //}
            }
        }

# endif

    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the raycast line.
        Vector3 raycastOrigin = transform.position + Vector3.down * transform.localScale.y * 0.5f;
        Vector3 raycastOrigin1 = transform.position + Vector3.right * transform.localScale.x * 0.5f;
        Vector3 raycastOrigin2 = transform.position + Vector3.left * transform.localScale.x * 0.5f;
        Gizmos.DrawRay(raycastOrigin, Vector3.down * 2f);
        Gizmos.DrawRay(raycastOrigin1, Vector3.down * 6f);
        Gizmos.DrawRay(raycastOrigin2, Vector3.down * 6f);
    }

    void ChangeColor()
    {
        switch (playerType)
        {
            case PlatformType.Blue:
                red.Play();
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[1];
                playerType = PlatformType.Red;
                gameObject.layer = 6;
                //scoreText.color = gameManager.platformMaterials[1].color;
                //textBackground.color = gameManager.platformMaterials[0].color;
                break;
            case PlatformType.Red:
                blue.Play();
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[0];
                playerType = PlatformType.Blue;
                gameObject.layer = 7;
                //scoreText.color = gameManager.platformMaterials[0].color;
                //textBackground.color = gameManager.platformMaterials[1].color;
                break;
        }

        //if (!audioSource1.isPlaying)
        //{
            audioSource1.PlayOneShot(colorSwap);
        //}
    }

    public void ChangeState()
    {
        switch (playerState)
        {
            case PlayerState.OutsidePlatform:
                playerState = PlayerState.InsidePlatform;
                break;
            case PlayerState.InsidePlatform:
                playerState = PlayerState.OutsidePlatform;
                break;
        }
    }

   private void OnCollisionEnter(Collision collision)
   {
       Vector3 raycastOrigin = transform.position; //+ Vector3.down * transform.localScale.y * 0.5f;
       Vector3 raycastOrigin1 = transform.position + Vector3.right * transform.localScale.x * 0.5f + Vector3.up* transform.localScale.y * 0.5f;
       Vector3 raycastOrigin2 = transform.position + Vector3.left * transform.localScale.x * 0.5f + Vector3.up * transform.localScale.y * 0.5f;
        RaycastHit hit;
       print("attempting Raycast");
       if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, 6f) || Physics.Raycast(raycastOrigin1, Vector3.down, out hit, 10f) || Physics.Raycast(raycastOrigin2, Vector3.down, out hit, 10f))
       {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            canJump = true;
            if(transform.position.y >= (hit.transform.position.y - 1f) & hit.transform.gameObject.layer == this.gameObject.layer)
            {
                Jump();
            }
       }
       else
        {
            canJump= false;
        }
   }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpMagnitude, ForceMode.VelocityChange);
        PlayerAnimation animation = GetComponent<PlayerAnimation>();
        //StartCoroutine(animation.ScaleCoroutine());
        StartCoroutine(animation.Hop());
        if(!audioSource.isPlaying )
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }


}

public enum PlayerState
{
    OutsidePlatform,
    InsidePlatform
}
