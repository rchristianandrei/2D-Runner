using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRandom : MonoBehaviour
{
    private Transform[] platforms;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private float minSpawnCD;
    [SerializeField]
    private float maxSpawnCD;

    private LevelManager levelManager;

    private void Start()
    {
        // Get reference to Level Manager
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        platforms = levelManager.platforms;

        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        while (!levelManager.isGameOver)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 trans = new(transform.position.x, platforms[Random.Range(0, platforms.Length)].position.y);
            Enemy enemyScript = Instantiate(enemy, trans, transform.rotation).GetComponent<Enemy>();
            enemyScript.runSpeed = levelManager.enemySpeed;

            yield return new WaitForSeconds(Random.Range(minSpawnCD, maxSpawnCD));
        }
    }
}
