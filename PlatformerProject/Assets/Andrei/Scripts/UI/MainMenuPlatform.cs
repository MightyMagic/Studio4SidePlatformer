using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlatform : MonoBehaviour
{
    [SerializeField] float jumpMagnitude;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hopSound;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpMagnitude, ForceMode.VelocityChange);

            source.PlayOneShot(hopSound);
        }
    }
}
