using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController_6ix7even : MonoBehaviour, MinigameSubscriber
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public int maxHealth = 3;
    private int currHealth;
    private Animator anim;

    void Start()
    {
        MinigameManager.Subscribe(this);
        MinigameManager.IsReady();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    void OnMove(InputValue val)
    {
        Vector2 input = val.Get<Vector2>();
        rb.linearVelocity = input * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        if(anim.GetBool("isDead")) return;

        currHealth -= amount;
        anim.SetTrigger("isHit");

        if(currHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        this.enabled = false;
        MinigameManager.EndGame();
        Destroy(gameObject, 0.5f);
    }

    public void OnMinigameStart() { Debug.Log("Minigame started!"); }
    public void OnTimerEnd()
    {
        MinigameManager.SetStateToFailure();
        Die();
    }
}
