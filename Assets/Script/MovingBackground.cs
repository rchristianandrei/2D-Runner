using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public float movingSpeed;

    private float border;

    // Reference
    private BoxCollider2D size;
    private LevelManager levelManager;

    private void Awake()
    {
        size = GetComponent<BoxCollider2D>();
        border = transform.position.x - (size.size.x / 4) * transform.localScale.x;
    }

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (levelManager.isGameOver)
            this.enabled = false;

        if (transform.position.x > border)
            transform.Translate(movingSpeed * Time.deltaTime * Vector2.left);
        else
            transform.position = new Vector2(0, transform.position.y);
    }
}
