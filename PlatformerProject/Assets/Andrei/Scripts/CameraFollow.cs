using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialOffset + player.transform.position;
    }
}
