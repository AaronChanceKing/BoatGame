using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<PlayerShips> Enemys;
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] GameObject EnemyPreFab;
    bool Spawning = true;

    [SerializeField] float[] SpawnTimes = new float[] { 10f, 8f, 6f, 4f, 2f};
    float TimeToSpawn = 5f;

    private void Start()
    {
        SetPlayer.GameOverEvent.AddListener(delegate { Spawning = false; });
    }

    private void Update()
    {
        if (!Spawning) return;

        TimeToSpawn -= Time.deltaTime;

        if(TimeToSpawn <= 0)
        {
            SetNewTime();
            SpawnEnemy();
        }
    }

    void SetNewTime()
    {
        float time = 0;

        switch(GameManager.Instance.GetDifficulty)
        {
            case GameManager.Difficulty.Easy:
                time = SpawnTimes[0];
                break;
            case GameManager.Difficulty.Medium:
                time = SpawnTimes[1];
                break;
            case GameManager.Difficulty.Hard:
                time = SpawnTimes[2];
                break;
            case GameManager.Difficulty.VeryHard:
                time = SpawnTimes[3];
                break;
            case GameManager.Difficulty.Godly:
                time = SpawnTimes[4];
                break;
        }

        TimeToSpawn = Random.Range(0, time);
    }
    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(EnemyPreFab);
        newEnemy.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
        newEnemy.GetComponent<Enemy>().SetEnemy(Enemys[Random.Range(0, Enemys.Count)]);
    }
}
