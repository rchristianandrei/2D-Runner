using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject heartUI;
    [SerializeField] private Transform heartPos;
    [SerializeField] private float spaceBetweenHearts;
    private float offSet = 120;

    private LevelManager levelManager;

    private Stack<GameObject> heartsObject = new();

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
            Destroy(heartsObject.Pop());

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

        heartsObject.Push(heart);

        heartPos.position = new Vector2(heartPos.position.x + offSet, heartPos.position.y);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
