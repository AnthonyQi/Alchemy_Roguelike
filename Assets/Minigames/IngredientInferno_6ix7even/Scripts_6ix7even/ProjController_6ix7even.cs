using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjController_6ix7even : MonoBehaviour
{
    public GameObject projectile;
    public float atkSpeed = 0.5f;
    public float ballSpeed = 20f;
    public float size = 1f;

    private float lastFireTime = 0f;

    void Update() 
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            Fire();
        }
    }

    void Fire() 
    {
        if (Time.time - lastFireTime < atkSpeed) return;
        lastFireTime = Time.time;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity);
        fireball.transform.localScale = Vector3.one * size;

        Vector2 direction = (mousePos - transform.position).normalized;

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);
        }

        SpriteRenderer sr = fireball.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = true;
    }
}
