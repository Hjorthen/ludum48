using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public bool SpawnTrees;
    public bool SpawnEnemies;

    public GameObject Terrain;
    public GameObject Player;
    public GameObject TreePrefab;
    public GameObject EnemyPrefab;
    public int InitialTiles = 5;
    
    private int NextTile = 0;

    private Queue<GameObject> WorldTiles = new Queue<GameObject>();
    // Start is called before the first frame update

    public void Start()
    {
        Terrain.SetActive(false);
    }

    private void SpawnNextChunk()
    {
        float spacing = Terrain.transform.localScale.x;
        Vector3 nextPosition = new Vector3(NextTile * spacing, 0, 0);
        GameObject newTile = GameObject.Instantiate(Terrain);
        newTile.transform.position = nextPosition;
        newTile.SetActive(true);
        NextTile++;
        WorldTiles.Enqueue(newTile);
        if(SpawnTrees)
            SpawnTreesOnTile(newTile);
        if(SpawnEnemies)
            SpawnEnemiesOnTile(newTile);
        if(WorldTiles.Count > 7)
        {
            RemoveChunk(WorldTiles.Dequeue());
        }
    }

    private void SpawnEnemiesOnTile(GameObject tile)
    {
        Vector3 scale = tile.transform.localScale;
        Vector3 pos = tile.transform.position;
        int spawnedEnemies = 0;
        for (int spawnAttempts = 0; spawnAttempts < 10000; spawnAttempts++)
        {
            float x = UnityEngine.Random.Range(0.00f, 1.0f);   
            float z = UnityEngine.Random.Range(0.25f, 0.75f);
            Vector3 spawnPosition = new Vector3(x, 0, z);
            bool locationOccupied = Physics.CheckSphere(spawnPosition, 0.50f, LayerMask.GetMask("Tree"));
            if(!locationOccupied)
            {
                var newEnemy = GameObject.Instantiate(EnemyPrefab, new Vector3(pos.x + scale.x * x, 1.0f, pos.z + scale.z * z), Quaternion.identity);
                spawnedEnemies++;
            }
            if(spawnedEnemies >= 3)
                break;
        }
    }

    private void SpawnTreesOnTile(GameObject tile)
    {
        Vector3 scale = tile.transform.localScale;
        Vector3 pos = tile.transform.position;
        int spawnedTrees = 0;
        for (int spawnAttempts = 0; spawnAttempts < 10000; spawnAttempts++)
        {
            float x = UnityEngine.Random.Range(0.00f, 1.0f);   
            float z = UnityEngine.Random.Range(0.25f, 0.75f);
            float sample = Mathf.PerlinNoise(x / 100, z / 100);
            if(sample > 0.3)
            {
                var newTree = GameObject.Instantiate(TreePrefab, new Vector3(pos.x + scale.x * x, 0.0f, pos.z + scale.z * z), Quaternion.identity);
                newTree.transform.SetParent(tile.transform);
                spawnedTrees++;
            }
            if(spawnedTrees >= 10)
                break;
        }
    }

    private void RemoveChunk(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }

    bool ShouldSpawnNextChunk()
    {
       return Player.transform.position.x + 5 * 20 > NextTile * 20; 
    }

    void Update()
    {
        if(ShouldSpawnNextChunk())
        {
            SpawnNextChunk();
        }
    }
}
