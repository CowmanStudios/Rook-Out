using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Spawner : Waypoint
{
    public Enemy testEnemyPrefab;

    [SerializeField]
    private float spawnDelay, spawnRate;

    public bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning after 3 seconds
        Invoke(nameof(StartSpawning), spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (spawning)
        {
            Enemy spawnedEnemy = Instantiate(testEnemyPrefab);
            spawnedEnemy.transform.position = transform.position;
            spawnedEnemy.targetWaypoint = Next;
            yield return new WaitForSeconds(spawnRate);
        }
    }


    // POLYMORPHISM
    // Draw the spawner icon
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "spawner", true);
    }
}
