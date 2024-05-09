using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private float difficultyIncreaseDelay;
    [SerializeField] private bool adjustSpawnRate;
    private int curDifficulty;


    private void Start()
    {
        curDifficulty = 0;
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseDelay, spawners.Length - 1);
    }

    private void IncreaseDifficulty()
    {
        curDifficulty++;
        if (curDifficulty < spawners.Length)
        {
            spawners[curDifficulty].SetActive(true);

            if (adjustSpawnRate)
            {
                for (int i = 0; i < curDifficulty; i++)     // Reduce old obstacle spawn rate to introduce new obstacle
                {
                    spawners[i].GetComponent<ProjectileSpawner>().spawnRate += 0.1f;
                }
            }
        }
    }
}
