using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatformLogic : MonoBehaviour
{
    TowerMovement towerMovement;

    // Start is called before the first frame update
    void Start()
    {
        towerMovement = GameObject.Find("Tower").GetComponent<TowerMovement>();
        
    }

   // private void OnCollisionEnter(Collision collision)
   // {
   //     towerMovement.isToucnhingVertical= true;
   // }
   // private void OnCollisionExit(Collision collision)
   // {
   //     towerMovement.isToucnhingVertical = false;
   // }


}
