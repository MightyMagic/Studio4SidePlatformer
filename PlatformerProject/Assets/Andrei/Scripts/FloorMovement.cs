using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] float delayBeforeMoving;
    public float verticalSpeed;
    public float maximumVerticalSpeed;

    [SerializeField] float speedIncreaseCoolDown;
    [SerializeField] float speedIncrement;
    float timerForSpeed = 0f;

    bool startMoving = false;

    GameManager gameManager;

    TowerMovement towerMovement;

    GameObject player;
    [SerializeField] float maxDistance;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        towerMovement = GameObject.Find("Tower").GetComponent<TowerMovement>();
        StartCoroutine(Wait(delayBeforeMoving));

        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (startMoving)
        {
            transform.position += new Vector3(0f, verticalSpeed, 0f) * Time.deltaTime;

            timerForSpeed += Time.deltaTime;

            if(timerForSpeed > speedIncreaseCoolDown)
            {
                timerForSpeed = 0f;
                if(verticalSpeed <= maximumVerticalSpeed)
                {
                    verticalSpeed += speedIncrement;
                }
            }
        }

        if(player.transform.position.y - transform.position.y > maxDistance)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - maxDistance, transform.position.z);
        }
       
    }

    IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        startMoving= true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            other.gameObject.SetActive(false);
            GameOver();
            //Destroy(other.gameObject);
            print("GameOver");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TowerSegment")
        {
            towerMovement.towerSegments.Remove(other.gameObject);
            Destroy(other.gameObject);
            
            print("GameOver");
        }

    }

    void GameOver()
    {
        if (PlayerPrefs.GetInt("Highscore", 0) < gameManager.scoreInt)
        {
            PlayerPrefs.SetInt("Highscore", gameManager.scoreInt);
        }

        SceneManager.LoadScene("Game Over");
    }
}
