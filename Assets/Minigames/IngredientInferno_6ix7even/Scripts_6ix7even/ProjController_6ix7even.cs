using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjController_6ix7even : MonoBehaviour
{
    public GameObject projectile;
    public float atkSpeed = 0.5f;
    public float ballSpeed = 20f;

    void Update() 
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Fire();
        }
    }

    void Fire() 
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // ensure z=0 for 2D

        // Instantiate the projectile at the player's position
        GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity);

        // Calculate direction
        Vector2 direction = (mousePos - transform.position).normalized;

        // Add force
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);
    }
}
