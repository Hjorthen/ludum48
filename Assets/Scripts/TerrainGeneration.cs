using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public bool SpawnTrees;
    public bool SpawnEnemies;
    public GameObject Backstop;
    public GameObject Terrain;
    public GameObject Player;
    public GameObject TreePrefab;
    public GameObject EnemyPrefab;
    public int InitialTiles = 5;


    
    [Tooltip("The amount of chunks in the beginning that will not have enemies spawned in them")]
    public float SpawnChunkCount;
    public float InitialChunkHealthPool;
    public float ChunkDifficultyScale;
    public float EnemyBaseHealth;


    public float InitialTreeCount;
    public float TreeScalingFactor;
    public int MaxTreeCount;

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
        WorldTiles.Enqueue(newTile);
        if(SpawnTrees)
            SpawnTreesOnTile(newTile);
        if(SpawnEnemies)
            SpawnEnemiesOnTile(newTile);
        if(WorldTiles.Count > 7)
        {
            RemoveChunk(WorldTiles.Dequeue());
            Backstop.transform.position = WorldTiles.Peek().transform.position;
        }
        NextTile++;
    }
    private void GetEnemyCountToSpawn(out int enemyCount, out float healthRatio)
    {
        if(NextTile < SpawnChunkCount)
        {
            enemyCount = 0;
            healthRatio = 0;
        }
        float chunkHealth = InitialChunkHealthPool * Mathf.Pow(ChunkDifficultyScale, NextTile - SpawnChunkCount);
        float spawnAmount = Mathf.Floor(chunkHealth / EnemyBaseHealth);
        float consumedHealth = spawnAmount * EnemyBaseHealth;
        float healthBoostRatio = chunkHealth / consumedHealth;
        enemyCount = Mathf.FloorToInt(spawnAmount);
        healthRatio = healthBoostRatio;
        Debug.Log($"Spawning {enemyCount} with ratio {healthRatio} for chunk {NextTile}");
    }
    private void SpawnEnemiesOnTile(GameObject tile)
    {
        Vector3 scale = tile.transform.localScale;
        Vector3 pos = tile.transform.position;
        int spawnedEnemies = 0;
        GetEnemyCountToSpawn(out int enemiesToSpawn, out float healthRatio);
        for (int spawnAttempts = 0; spawnAttempts < 10000; spawnAttempts++)
        {
            if(spawnedEnemies >= enemiesToSpawn)
                break;

            float x = UnityEngine.Random.Range(0.00f, 1.0f);   
            float z = UnityEngine.Random.Range(0.25f, 0.75f);
            Vector3 spawnPosition = new Vector3(x, 0, z);
            bool locationOccupied = Physics.CheckSphere(spawnPosition, 0.50f, LayerMask.GetMask("Tree"));
            if(!locationOccupied)
            {
                var newEnemy = GameObject.Instantiate(EnemyPrefab, new Vector3(pos.x + scale.x * x, 1.0f, pos.z + scale.z * z), Quaternion.identity);
                Health health = newEnemy.GetComponent<Health>();
                health.health = EnemyBaseHealth * healthRatio;
                spawnedEnemies++;
            }
        }
    }

    private int GetTreeCountToSpawn()
    {
        float scaledTreeCount = InitialTreeCount * Mathf.Pow(TreeScalingFactor, NextTile);

        // Return a random between the two for some diversity
        int treeCount = UnityEngine.Random.Range(Mathf.FloorToInt(scaledTreeCount), Mathf.CeilToInt(scaledTreeCount));
        return Math.Min(treeCount, MaxTreeCount);
    }

    private void SpawnTreesOnTile(GameObject tile)
    {
        Vector3 scale = tile.transform.localScale;
        Vector3 pos = tile.transform.position;
        int spawnedTrees = 0;
        int treesToSpawn = GetTreeCountToSpawn();
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
            if(spawnedTrees >= treesToSpawn)
                break;
        }
    }

    private void RemoveChunk(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }

    bool ShouldSpawnNextChunk()
    {
        if (Player != null)
        {
            return Player.transform.position.x + 5 * 20 > NextTile * 20;
        }

        return false;
    }

    void Update()
    {
        if(ShouldSpawnNextChunk())
        {
            SpawnNextChunk();
        }
    }
}
