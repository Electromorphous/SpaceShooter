using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject hitPrefab;
    public int damage;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        if (transform.position.y > 17 || transform.position.y < -17)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Vector2 hitPosition = transform.position;
        
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Player player = hitInfo.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);

        Instantiate(hitPrefab, hitPosition, Quaternion.identity);

    }
}
