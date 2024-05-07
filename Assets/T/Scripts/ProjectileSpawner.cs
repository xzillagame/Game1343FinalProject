using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ProjectileSpawner : MonoBehaviour
{
    public float spawnRate;
    [SerializeField] private GameObject projectile;
    private float spawnerWidth;

    private void Start()
    {
        spawnerWidth = Helper_GetPixelWidth(this.gameObject);
        StartCoroutine(SpawnObstaclesCoroutine());
    }

    private IEnumerator SpawnObstaclesCoroutine()
    {
        while (true)
        {
            Spawner_SpawnObstacle();
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void Spawner_SpawnObstacle()
    {
        try
        { 
            GameObject obstacle = Instantiate(projectile, transform.position, transform.rotation);

            float obstacleWidth = Helper_GetPixelWidth(obstacle);
            float xMinRange = transform.position.x - (spawnerWidth / 2) + (obstacleWidth / 2);
            float xMaxRange = transform.position.x + (spawnerWidth / 2) - (obstacleWidth / 2);
            float xObstacleNewPosition = Random.Range(xMinRange, xMaxRange);

            obstacle.transform.position = new Vector2(xObstacleNewPosition, obstacle.transform.position.y);
            obstacle.SetActive(true);
        }
        catch
        {
            Debug.LogError("Error: Missing projectile from " + this.name);
        }
    }


    private float Helper_GetPixelHeight(GameObject target)
    {
        try
        {
            return target.GetComponent<SpriteRenderer>().bounds.size.y;
        }
        catch
        {
            Debug.LogError("Projectile missing Sprite Renderer component");
            return -1;
        }
    }
    private float Helper_GetPixelWidth(GameObject target)
    {
        try
        {
            return target.GetComponent<SpriteRenderer>().bounds.size.x;
        }
        catch
        {
            Debug.LogError("Projectile missing Sprite Renderer component");
            return -1;
        }
    }
}
