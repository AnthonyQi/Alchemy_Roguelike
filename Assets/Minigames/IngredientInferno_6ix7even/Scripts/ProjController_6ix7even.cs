
using UnityEngine;
using UnityEngine.InputSystem;

/*
    This is an example script part of the debug minigame

    The purpose of it is to show you how to properly deal with input
    and use the provided MinigameManager.cs class
*/

[RequireComponent(typeof(Rigidbody2D))]
public class ProjController_6ix7even : MonoBehaviour
{
 private Rigidbody2D rb;
   public GameObject projectile;
   public float atkSpeed = 0.5f;
   public float ballSpeed = 700f;

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }
    void Fire() {
            Vector3 mousePos = Input.mousePosition;
            Rigidbody2D fireball = Instantiate(rb, mousePos);
            fireball.GetComponent<Rigidbody2D>().AddForce(mousePos.x*ballSpeed);
            coolDown = Time.time + attackSpeed;
            return;
    }

}
