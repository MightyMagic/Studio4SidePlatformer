using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    [SerializeField] TowerMovement towerSpawn;
    [SerializeField] GameObject starPrefab;
    [SerializeField] GameObject emptyObject;

    [SerializeField] GameObject player;
    [SerializeField] GameManager gameManager;

    [SerializeField] float coolDown;
    float timeUp;
    float timer = 0f;

    GameObject starAnchor;
    GameObject newSphere;
    GameObject towerSegment;
    float startX;
    float startZ;
    int currentTowerSegments;

    void Start()
    {
        startX = player.transform.position.x;
        startZ = player.transform.position.z;

        timeUp = Random.Range(coolDown, coolDown * 2f);
    }

    void Update()
    {
        if(timer < timeUp & !gameManager.starPresent)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            timeUp = Random.Range(coolDown, coolDown * 3f);
            if(!gameManager.starPresent)
            {
                gameManager.starPresent = true;
                SpawnStar();
            }
        }
    
    }

    void SpawnStar()
    {
        currentTowerSegments = towerSpawn.towerSegments.Count;
      
        if (towerSpawn.towerSegments.Count > 0)
        {
            towerSegment = towerSpawn.towerSegments[currentTowerSegments - 1];


            newSphere = Instantiate(starPrefab, new Vector3(startX, Random.Range(towerSegment.transform.position.y - (towerSegment.transform.localScale.y /2), 
                towerSegment.transform.position.y + (towerSegment.transform.localScale.y / 2)), startZ), Quaternion.identity);

            starAnchor = Instantiate(emptyObject, towerSegment.transform.position, Quaternion.identity);
            starAnchor.transform.SetParent(towerSegment.transform);

            newSphere.transform.SetParent(starAnchor.transform);

        }
    }
}
