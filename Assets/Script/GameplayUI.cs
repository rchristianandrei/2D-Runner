using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject heartUI;
    [SerializeField] private Transform heartPos;
    [SerializeField] private float spaceBetweenHearts;
    private float offSet = 120;

    private LevelManager levelManager;

    private Queue<GameObject> heartsObject = new Queue<GameObject>();

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        for (int i = 0; i < levelManager.playerMaxHealth; i++)
        {
            SpawnHeart();
        }
    }

    public void DecreaseHeart(int value)
    {
        if (value > heartsObject.Count)
            value = heartsObject.Count;

        int count = heartsObject.Count - value;

        for (int i = heartsObject.Count - 1; i >= count; i--)
        {    
            Destroy(heartsObject.Dequeue());

            heartPos.position = new Vector2(heartPos.position.x - offSet, heartPos.position.y);
        }
    }

    public void IncreaseHeart(int value)
    {
        int remaining = levelManager.playerMaxHealth - heartsObject.Count;
        if (value > remaining)
            value = remaining;

        for (int i = 0; i < value; i++)
        {
            SpawnHeart();
        }
    }

    private void SpawnHeart()
    {
        GameObject heart = Instantiate(heartUI, heartPos.position, heartPos.rotation, gameObject.transform);

        heartsObject.Enqueue(heart);

        heartPos.position = new Vector2(heartPos.position.x + offSet, heartPos.position.y);
    }
}
