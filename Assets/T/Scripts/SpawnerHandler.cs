using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private float difficultyIncreaseDelay;
    private int curDifficulty;


    private void Start()
    {
        try
        {
            curDifficulty = 0;
            InvokeRepeating("IncreaseDifficulty", difficultyIncreaseDelay, spawners.Length - 1);
        }
        catch
        {
            Debug.LogError("Error: Need to fill in Spawners List");
        }
    }


    private void IncreaseDifficulty()
    {
        curDifficulty++;
        if (curDifficulty < spawners.Length)
        {
            spawners[curDifficulty].SetActive(true);

            //for (int i = 0; i < curDifficulty; i++)     // Reduce old obstacle spawn speed to introduce new obstacle
            //{
            //    print(i);
            //    spawners[i].GetComponent<ProjectileSpawner>().spawnRate += 0.2f;
            //}
        }
    }
}
