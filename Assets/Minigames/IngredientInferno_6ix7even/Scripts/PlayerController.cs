using UnityEngine;
using UnityEngine.InputSystem;

/*
    This is an example script part of the debug minigame

    The purpose of it is to show you how to properly deal with input
    and use the provided MinigameManager.cs class
*/

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))] // This component must be attached to the GameObject for input to register
public class PlayerController : MonoBehaviour, MinigameSubscriber
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public int maxHealth = 3;
    private int currHealth; 
    private Animator anim;

    void Start()
    {
        // Subscribes this class to the minigame manager. This gives access to the
        // 'OnMinigameStart()' and 'OnTimerEnd()' functions. Otherwise, they won't be called.
        MinigameManager.Subscribe(this);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    void OnInteract(InputValue val)
 {
 if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        MinigameManager.SetStateToSuccess(); // Change the minigame state to "Success"
        MinigameManager.EndGame(); // End the minigame. Without this, the minigame would end when the timer finishes instead (still with success).
    }

    void OnMove(InputValue val) {
        if (!MinigameManager.IsReady()) // IMPORTANT: Don't allow any input while the countdown is still occuring
            return;

        Vector2 input = val.Get<Vector2>(); // Get the Vector2 that represents input
        rb.linearVelocity = input * speed; // 5f is a magic number; speed.
    }
    
      void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
        }
    }
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            TakeDamage(1);
        }
    }

    private void Die() {
        Destroy(gameObject);
        anim.SetBool("isDead", true);
        this.enabled = false;
        MinigameManager.EndGame();
    }

    void TakeDamage(int amount){
        if(anim.GetBool("isDead")) return;

        currHealth -= amount;
        anim.SetTrigger("isHit");

        if(currHealth <= 0){
            Die();
        }
        
    }


    public void OnMinigameStart()
    {
        Debug.Log("Minigame started!");
        // There isn't anything interesting that needs to happen in here for this example
    }

    public void OnTimerEnd()
    {
        // Timer has expired
        MinigameManager.SetStateToFailure();
        Die();
    }
}
