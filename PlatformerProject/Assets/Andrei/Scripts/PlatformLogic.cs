using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour
{
    public PlatformType platformType;
    GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        switch (platformType)
        {
            case PlatformType.Blue:
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[0];
                //gameObject.layer = (int)gameManager.blueMask;
                gameObject.layer = 7;
                break;
            case PlatformType.Red:
                this.gameObject.GetComponent<Renderer>().material = gameManager.platformMaterials[1];
                //gameObject.layer = (int)gameManager.redMask;
                gameObject.layer = 6;
                break;
        }
    }

}
