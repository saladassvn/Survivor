using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public string waveName;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
    public float maxEnemy;
    public string nextWaveTime;
}

public class WaveSpawner : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Wave[] waves;
    Wave currentWave;
    int currentWaveNumber;
    Camera cam;
    float nextSpawnTime;

    [SerializeField] float spawnRadius;

    public Queue<GameObject> QueuedEnemy;

    private void Start()
    {
        cam = Camera.main;
        spawnRadius = cam.orthographicSize + 4;

        QueuedEnemy = new Queue<GameObject>(); 
    }
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        if (currentWave.nextWaveTime == timerText.text)
        {
            currentWaveNumber++;
            return;
        }
        SpawnWave();
    }

    void SpawnWave()
    {
        if (nextSpawnTime < Time.time && GameObject.FindGameObjectsWithTag("Enemy").Length < currentWave.maxEnemy)
        {
            GameObject ranEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
            GameObject newEnemy = Instantiate(ranEnemy, spawnPos, Quaternion.identity);
            newEnemy.transform.SetParent(transform);
            nextSpawnTime = Time.time + currentWave.spawnInterval;


        }

        //if (nextSpawnTime < Time.time && GameObject.FindGameObjectsWithTag("Enemy").Length < currentWave.maxEnemy)
        //{
        //    if (QueuedEnemy.Count > 0)
        //    {
        //        GameObject currentEnemy = QueuedEnemy.Dequeue();
        //        Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        //        currentEnemy.transform.position = spawnPos;
        //        currentEnemy.transform.SetParent(transform);

        //        nextSpawnTime = Time.time + currentWave.spawnInterval;
        //    }
        //    else
        //    {
        //        GameObject enemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
        //        Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        //        GameObject currentEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
        //        currentEnemy.transform.position = spawnPos;
        //        currentEnemy.transform.SetParent(transform);
        //        nextSpawnTime = Time.time + currentWave.spawnInterval;
        //    }
        //}

    }

}
