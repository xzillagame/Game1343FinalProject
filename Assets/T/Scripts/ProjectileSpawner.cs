using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public float spawnRate;
    [SerializeField] private GameObject[] projectileVariants;
    [SerializeField] private Transform player;
    [SerializeField] private bool isRandomRotation = true;
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
        GameObject p_variant = projectileVariants[(int)Random.Range(0, projectileVariants.Length)]; // Skin choosing
        GameObject projectile = Instantiate(p_variant, transform.position, transform.rotation);
        Helper_SetPositionAndRotation(projectile);
    }

    private void Helper_SetPositionAndRotation(GameObject projectile)
    {
        float obstacleWidth = 0;
        if (player != null) // Special case for Missle projectile
        {
            obstacleWidth = Helper_GetPixelWidth(projectile.transform.GetChild(0).gameObject);
            projectile.GetComponent<ProjectileMissle>().SetPlayerTransform(player);
        }
        else
        {
            obstacleWidth = Helper_GetPixelWidth(projectile);
        }

        float xMinRange = transform.position.x - (spawnerWidth / 2) + (obstacleWidth / 2);
        float xMaxRange = transform.position.x + (spawnerWidth / 2) - (obstacleWidth / 2);
        Vector3 randPosition = new Vector3(Random.Range(xMinRange, xMaxRange), projectile.transform.position.y);
        Vector3 randRotation = Vector3.forward * Random.Range(0, 360);

        projectile.transform.position = randPosition;
        if(isRandomRotation) projectile.transform.eulerAngles = randRotation;
    }
    /* Beware error when not plug in Player transform for Missle Spawner */
    private float Helper_GetPixelWidth(GameObject target)
    {
        return target.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
