using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    TowerMovement towerMovement;
    public List<GameObject> towerSegments;
    GameObject player;

    public float segmentSize;
    float spawnDistance;

    float initialY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        towerMovement = GetComponent<TowerMovement>();
        //segmentSize = towerSegment.GetComponent<Renderer>().bounds.max.y;
        segmentSize = 2 * towerSegments[0].transform.localScale.y;

        spawnDistance= segmentSize * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if( Mathf.Abs(player.transform.position.y - towerMovement.towerSegments[towerMovement.towerSegments.Count - 1].transform.position.y) < spawnDistance)
        {
            //SpawnNewSegment();
            SpawnSegment();
        }
        
    }

    void SpawnNewSegment()
    {
        int rand = Random.Range(0, towerMovement.towerSegments.Count);

        segmentSize = 2 * towerMovement.towerSegments[rand].transform.localScale.y;
        Vector3 spawnPosition = towerMovement.towerSegments[towerMovement.towerSegments.Count - 1].transform.position + new Vector3(0, segmentSize, 0);

        float randAngle = Random.Range(0f, 180f);
        Quaternion angle = Quaternion.Euler(0, randAngle, 0);
        towerMovement.towerSegments.Add(Instantiate(towerMovement.towerSegments[rand], spawnPosition, angle));
    }

    void SpawnSegment()
    {
        int rand = Random.Range(0, towerSegments.Count);

        segmentSize = 2 * towerMovement.towerSegments[rand].transform.localScale.y;
        Vector3 spawnPosition = towerMovement.towerSegments[towerMovement.towerSegments.Count - 1].transform.position + new Vector3(0, segmentSize, 0);

        float randAngle = Random.Range(0f, 180f);
        Quaternion angle = Quaternion.Euler(0, randAngle, 0);
        towerMovement.towerSegments.Add(Instantiate(towerSegments[rand], spawnPosition, angle));
    }
}
