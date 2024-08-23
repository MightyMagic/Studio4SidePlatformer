using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Material> platformMaterials;
    public TextMeshProUGUI score;
    public int scoreInt = 0;

    float yCoord;
    [SerializeField] float heightToScore;
    float InitialHeightToUpScore;


    [SerializeField] float heightToUpScore;
    [SerializeField] float floorSpeedIncrement;

    public FloorMovement floor;

    GameObject player;

    public LayerMask blueMask;
    public LayerMask redMask;

    [SerializeField] List<GameObject> gameObjectsToReset;
    [SerializeField] TowerMovement towerScript;
   
    [SerializeField] float yToReset;
    public List<GameObject> currentObjectsToReset = new List<GameObject>();

    public JobSystemNew jobSystem;

    public bool starPresent = false;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + scoreInt;

        player = GameObject.Find("Player");
        floor = GameObject.Find("Floor").GetComponent<FloorMovement>();

        yCoord = player.transform.position.y;
        InitialHeightToUpScore= heightToUpScore;

        PlayerPrefs.GetInt("Highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position.y - yCoord) > heightToScore)
        {
            scoreInt += 100;
            yCoord += heightToScore;
            score.text = "Score: " + scoreInt;
            // Apparently for some reason this can only be performed on the main thread
            //jobSystem.ScheduleJob(new UpdateUIJob(score, scoreInt));
        }

        if(yCoord > heightToUpScore)
        {
            heightToUpScore += InitialHeightToUpScore;
            jobSystem.ScheduleJob(new IncreaseFloorSpeedJob(this, floorSpeedIncrement));
            //IncreaseFloorSpeed();

        }

        if(player.transform.position.y > yToReset)
        {
            // Allocating YReset to another thread
            //ResetY(yToReset);
            jobSystem.ScheduleJob(new ResetYJob(this, yToReset));
        }
    }

    public void IncreaseFloorSpeed()
    {
        if(floor.verticalSpeed <= floor.maximumVerticalSpeed)
        {
            floor.verticalSpeed += floorSpeedIncrement;
        }
    }

    public void ResetY(float yBack)
    {
        currentObjectsToReset.Clear();

        for(int i = 0; i < gameObjectsToReset.Count; i++)
        {
            currentObjectsToReset.Add(gameObjectsToReset[i]);
        }

        for(int j = 0; j < towerScript.towerSegments.Count; j++)
        {
            currentObjectsToReset.Add((towerScript.towerSegments[j]));
        }
     

        for(int i = 0; i < currentObjectsToReset.Count; i++)
        {
            GameObject go = currentObjectsToReset[i];
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - yBack, go.transform.position.z);
        }

        yCoord -= yToReset;
    }

    public void AddScore(int increment)
    {
        scoreInt += increment;
    }

}

public class ResetYJob : IJob
{
    private GameManager _gameManager;
    private float _yBack;

    public ResetYJob(GameManager gameManager, float yBack)
    {
        _gameManager = gameManager;
        _yBack = yBack;
    }

    public void Execute()
    {
        _gameManager.ResetY(_yBack);
    }
}

public class IncreaseFloorSpeedJob: IJob
{
    private FloorMovement floorMovement;
    private float floorSpeedIncrement;

    public IncreaseFloorSpeedJob(GameManager manager, float floorSpeedIncrement)
    {
        floorMovement = manager.floor;
        this.floorSpeedIncrement = floorSpeedIncrement;
    }

    public void Execute()
    {
        if (floorMovement.verticalSpeed <= floorMovement.maximumVerticalSpeed)
        {
            floorMovement.verticalSpeed += floorSpeedIncrement;
        }
    }    
}

public class UpdateUIJob: IJob
{
    private TextMeshProUGUI _scoreText;
    private int _scoreValue;

    public UpdateUIJob(TextMeshProUGUI scoreText, int scoreValue)
    {
        _scoreText = scoreText;
        _scoreValue = scoreValue;
    }

    public void Execute()
    {
        _scoreText.text = "Score: " + _scoreValue;
    }
}



public enum PlatformType
{
    Red,
    Blue
}
