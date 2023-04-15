using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // HP
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    // Attack Range
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackPointRadius;

    // Platforms
    [SerializeField]
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

        currentHealth = maxHealth;
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
        else if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");

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

        if (currentHealth <= 0)
            Dead();
    }

    private void Dead()
    {
        animator.SetBool("Dead", true);

        detector.enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
