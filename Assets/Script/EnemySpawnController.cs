using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Collider2D skyArea;
    [SerializeField] private Collider2D groundArea;
    [SerializeField] private Transform player;
    [SerializeField] private float safeDistance = 3f; 

    private float timer;
    private float totalRate;
    private Dictionary<Enemy, List<GameObject>> enemyPools = new Dictionary<Enemy, List<GameObject>>();

    void Start()
    {
        totalRate = 0f;
        foreach (var enemyData in enemyList)
        {
            totalRate += enemyData.GetRate();
            enemyPools[enemyData] = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject o = Instantiate(enemyData.GetPrefab());
                o.SetActive(false);
                enemyPools[enemyData].Add(o);
            }
        }
    }

    Enemy GetEnemyByRate()
    {

        float randomPoint = Random.Range(0f, totalRate);
        float cumulative = 0f;

        foreach (var e in enemyList)
        {
            cumulative += e.GetRate();
            if (randomPoint <= cumulative)
            {
                return e; 
            }
        }
        return enemyList[enemyList.Count - 1];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }
    void SpawnEnemy()
    {
        Enemy enemyData = GetEnemyByRate();
        GameObject enemy = GetFreeEnemy(enemyData);
        if (enemy == null) return;

        enemy.transform.position = GetRandomSpawnPosition(enemyData);
        enemy.SetActive(true);
    }

    Vector3 GetRandomSpawnPosition(Enemy enemyData)
    {
        if (enemyData.IsFlyable())
        {
            return GetRandomPointInCollider(skyArea);
        }
        else
        {
            return GetRandomPointInCollider(groundArea);
        }
    }

    Vector3 GetRandomPointInCollider(Collider2D col)
    {
        Bounds bounds = col.bounds;
        Vector3 point;

        int maxAttempts = 10; 
        int attempts = 0;

        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            point = new Vector3(x, y, 0f);

            attempts++;
        }
        while (Vector3.Distance(point, player.position) < safeDistance && attempts < maxAttempts);

        return point;
    }



    GameObject GetFreeEnemy(Enemy enemyData)
    {
        foreach (var enemy in enemyPools[enemyData])
        {
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }
        return null; 
    }
}
