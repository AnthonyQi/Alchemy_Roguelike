using UnityEngine;

public class RangeEnemyController : MonoBehaviour
{
    #region Instance Variables
    public float speed = 2f;
    public int maxHealth = 5;
    private int currentHealth;
    private Animator anim;
    private Transform player;
    #endregion

    void Start() {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if(anim.GetBool("isDead")){
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        float stopDistance = 4f;

        if(distance < 15f && distance > stopDistance) {
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else {
            anim.SetBool("isWalking", false);
        }
    }


    public void TakeDamage(int amount) {
        if(anim.GetBool("isDead")) return;

        currentHealth -= amount;
        anim.SetTrigger("isHit");

        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
