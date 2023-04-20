using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int playerStarterHeartMax;
    [HideInInspector] public int playerStarterHeart;

    public static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetPlayerHeart;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetPlayerHeart;
    }

    private void ResetPlayerHeart(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            playerStarterHeart = playerStarterHeartMax;
        }
    }
}
