using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runSpeed;
    [SerializeField]
    private int damage;

    [SerializeField]
    private GameObject explosionEffect;

    // Reference
    private Animator animator;
    private LevelManager levelManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.isGameOver)
            transform.Translate(runSpeed * Time.deltaTime * Vector2.left);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    public void DestroySelf()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
