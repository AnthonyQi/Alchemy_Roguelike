using UnityEngine;

public class MeleeEnemyController_6ix7even : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 5;
    private int currentHealth;
    private Animator anim;
    public GameObject player;

    private Vector2 randomDir;
    private float moveTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        PickRandomDirection();
    }

    void Update()
    {
        if (anim.GetBool("isDead")) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 5f)
        {
            anim.SetBool("isWalking", true);
            Vector2 dir = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            MoveRandomly();
        }
    }

    void MoveRandomly()
    {
        anim.SetBool("isWalking", true);
        transform.Translate(randomDir * speed * Time.deltaTime);

        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0f)
        {
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        randomDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        moveTimer = Random.Range(0.5f, 3f);
    }

    public void TakeDamage(int amount)
    {
        if (anim.GetBool("isDead")) return;

        currentHealth -= amount;
        anim.SetTrigger("isHit");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
