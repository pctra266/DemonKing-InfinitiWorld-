using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public float chunkWidth = 20f;
    private float nextSpawnRightX = 0f;
    private float nextSpawnLeftX = 0f;
    [SerializeField]
    private Transform cam; // camera transform

    void Start()
    {
        cam = Camera.main.transform;

        // Spawn mảnh đầu tiên ở vị trí 0
        GameObject firstChunk = Instantiate(
            gridPrefabs[Random.Range(0, gridPrefabs.Length)],
            Vector3.zero,
            Quaternion.identity
        );

        nextSpawnRightX = chunkWidth;
        nextSpawnLeftX = -chunkWidth;
    }

    void Update()
    {
        float camX = cam.position.x;

        // Spawn bên phải nếu camera gần rìa phải
        if (camX > nextSpawnRightX - chunkWidth)
        {
            SpawnChunkRight();
        }

        // Spawn bên trái nếu camera gần rìa trái
        if (camX < nextSpawnLeftX + chunkWidth)
        {
            SpawnChunkLeft();
        }
    }

    void SpawnChunkRight()
    {
        GameObject prefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];
        Instantiate(prefab, new Vector3(nextSpawnRightX, 0, 0), Quaternion.identity);
        nextSpawnRightX += chunkWidth;
    }

    void SpawnChunkLeft()
    {
        GameObject prefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];
        Instantiate(prefab, new Vector3(nextSpawnLeftX, 0, 0), Quaternion.identity);
        nextSpawnLeftX -= chunkWidth;
    }
}

