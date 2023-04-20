using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Level Settings
    [HideInInspector] public int playerMaxHealth;
    [SerializeField] private float playerAttackRate;
    [SerializeField] private float backgroundSpeed = 2;
    [SerializeField] private float groundSpeed = 3;

    public float MaxProgress;
    public float currentProgress = 0;

    public float enemySpeed = 3;

    // Level Prefabs
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject playerPrefab;

    // References
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject gameplayScreen;

    // Player
    private PlayerController playerController;

    // Flags
    public bool isGameOver { get; set; } = false;

    // Platforms
    public Transform[] platforms;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        // If Player Data exist
        if (PlayerData.instance)
            playerMaxHealth = PlayerData.instance.playerStarterHeart;

        // Spawns Background
        GameObject background = Instantiate(backgroundPrefab, Vector2.zero, backgroundPrefab.transform.rotation);
        background.GetComponent<MovingBackground>().movingSpeed = backgroundSpeed;

        // Spawn ground
        foreach (Transform t in platforms)
        {
            GameObject ground = Instantiate(groundPrefab, new Vector3(0, t.position.y - 1.43f, t.position.z), groundPrefab.transform.rotation);
            ground.GetComponent<MovingBackground>().movingSpeed = groundSpeed;
        }

        // Spawn Player
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        playerController = player.GetComponent<PlayerController>();
        playerController.SetMaxHealth(playerMaxHealth);
        playerController.attackRate = playerAttackRate;
    }

    private void Update()
    {
        currentProgress += Time.deltaTime;

        if (currentProgress >= MaxProgress)
        {
            isGameOver = true;

            // You Won Screen
            Win();
        }
    }

    public void Win()
    {
        PlayerData.instance.playerStarterHeart = playerController.currentHealth;
        gameplayScreen.SetActive(false);
        winScreen.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>().enabled = false;
    }

    public void Gameover()
    {
        gameplayScreen.SetActive(false);
        loseScreen.SetActive(true);
    }
}
