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

    private void Start()
    {
        // Get reference to platforms
        platforms = GameObject.Find("LevelManager").GetComponent<LevelManager>().platforms;

        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        while (true)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 trans = new(transform.position.x, platforms[Random.Range(0, platforms.Length)].position.y);
            Instantiate(enemy, trans, transform.rotation);

            yield return new WaitForSeconds(Random.Range(minSpawnCD, maxSpawnCD));
        }
        
    }
}
