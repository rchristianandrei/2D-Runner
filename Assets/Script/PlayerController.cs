using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // HP
    private int maxHealth;
    private int currentHealth;

    // Attack Parameters
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackPointRadius;

    public float attackRate = 0.5f;
    private float nextAttack = 0;

    // Platforms
    private Transform[] grounds;
    private int currentIndex = 0;

    // Keys
    [SerializeField]
    private KeyCode shootKey;
    [SerializeField]
    private KeyCode jumpUpKey;
    [SerializeField]
    private KeyCode jumpDownKey;

    // References
    private Animator animator;
    private Collider2D detector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        detector = GetComponent<Collider2D>();  
    }

    private void Start()
    {
        // Get reference to platforms
        grounds = GameObject.Find("LevelManager").GetComponent<LevelManager>().platforms;
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpUpKey))
        {
            Jump(true);
        }
        else if (Input.GetKeyDown(jumpDownKey))
        {
            Jump(false);
        }
        else if (Input.GetKeyDown(shootKey) && Time.time >= nextAttack)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");

        nextAttack = Time.time + 1 / attackRate;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius, LayerMask.GetMask("Killable"));

        foreach (Collider2D c in enemies)
        {
           c.GetComponent<Enemy>().DestroySelf();
        }
    }

    private void Jump(bool toUp)
    {
        if (toUp)
        {
            if (currentIndex < grounds.Length - 1)
                currentIndex += 1;
        }
        else
        {
            if (currentIndex > 0)
                currentIndex -= 1;
        }

        transform.position = new Vector2(transform.position.x, grounds[currentIndex].position.y);
    }

    public void TakeDamage(int damage)
    {
        // Play hurt animation
        animator.SetTrigger("Hurt");
        // Reduce Life
        currentHealth -= damage;
        GameObject.Find("Gameplay Canvas").GetComponent<GameplayUI>().DecreaseHeart(damage);

        if (currentHealth <= 0)
            Dead();
    }

    private void Dead()
    {
        animator.SetBool("Dead", true);

        // Set flag to gameover
        LevelManager lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (lvlManager)
            lvlManager.isGameOver = true;

        // Off collider and script
        detector.enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        currentHealth = value;
    }
}
