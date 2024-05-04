using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private float obstacleLevel;
    private float spawnerWidth, spawnRate;

    private void Awake()
    {
        obstacleLevel = 1;
    }
    private void Start()
    {
        spawnerWidth = GetPixelWidth(this.gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) { SpawnObstacle(); }
    }
    /* How it work:
    1. Spawn obstacle type depend on current difficulty level
    2. Calculate and assign new position (Position range is size of the spawner)
    3. Activate obstacle to avoid possible collision when instantiating
     */
    private void SpawnObstacle()
    {
        int obstacleIndex = (int)Random.Range(0, obstacleLevel);
        GameObject obstacle = Instantiate(obstacles[obstacleIndex], transform.position, transform.rotation);

        float obstacleWidth = GetPixelWidth(obstacle);
        float xMinRange = transform.position.x - (spawnerWidth / 2) + (obstacleWidth / 2);
        float xMaxRange = transform.position.x + (spawnerWidth / 2) - (obstacleWidth / 2);
        float xObstacleNewPosition = Random.Range(xMinRange, xMaxRange);

        obstacle.transform.position = new Vector2(xObstacleNewPosition, obstacle.transform.position.y);
        obstacle.SetActive(true);   
    }
    private void IncreaseOstacleLevel()
    {
        if(obstacleLevel + 1 < obstacles.Length)
            obstacleLevel++;
    }

    /*
    Requirement: Target NEED to have a SpriteRenderer component
    Error: Default error handler is better 
     */
    private float GetPixelHeight(GameObject target)
    {
        return target.GetComponent<SpriteRenderer>().bounds.size.y;
    }
    private float GetPixelWidth(GameObject target)
    {
        return target.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
