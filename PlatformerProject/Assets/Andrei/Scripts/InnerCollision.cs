using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerCollision : MonoBehaviour
{
    private Player player;
    public PlatformType playerType;
    public PlatformType platformType;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerType = player.playerType;
        platformType = transform.parent.GetComponent<PlatformLogic>().platformType;

    }

    void OnTriggerEnter(Collider other)
    {
         player.ChangeState();
        //CheckPlatform();

    }



    void OnTriggerExit(Collider other)
    {
         player.ChangeState();
        //CheckPlatform();
    }

}
