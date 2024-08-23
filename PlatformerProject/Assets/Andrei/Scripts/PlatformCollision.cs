using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    private Player player;
    public PlatformType playerType;
    public PlatformType platformType;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerType = player.playerType;
        platformType = transform.parent.GetComponent<PlatformLogic>().platformType;
        
    }

   // void OnTriggerEnter(Collider other)
   // {
   //    // player.ChangeState();
   //     CheckPlatform();
   //     
   // }

  //  void OnTriggerStay(Collider other)
  //  {
  //      CheckPlatform();
  //  }

   // void OnTriggerExit(Collider other)
   // {
   //    // player.ChangeState();
   //     CheckPlatform();
   // }

   // void CheckPlatform()
   // {
   //     playerType = player.playerType;
   //     platformType = transform.parent.GetComponent<PlatformLogic>().platformType;
   //
   //    // print("Entered platform area, platform is " + platformType + " and player is " + playerType);
   //
   //     if (playerType != platformType)
   //     {
   //         transform.parent.GetComponent<Collider>().enabled = false;
   //     }
   //     else
   //     {
   //         transform.parent.GetComponent<Collider>().enabled = true;
   //     }
   // }
}
