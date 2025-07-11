using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Control : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force2;
    public float dmg2;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos ;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force2;
        float rot = Mathf.Atan2(rotation.y , rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot + 90);
    }

    // Update is called once per frame
    void Update(){
        Destroy(gameObject,1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other is CapsuleCollider2D)
            {
                // Get the EnemyController component from the enemy
                EnemyController enemyController = other.GetComponent<EnemyController>();

                // Check if the enemy has an EnemyController component
                if (enemyController != null)
                {
                    // Apply damage to the enemy
                    enemyController.TakeDamage(dmg2);
                }

                // Destroy the bullet after it hits the enemy
                Destroy(gameObject);
            }
        }
    }
}
