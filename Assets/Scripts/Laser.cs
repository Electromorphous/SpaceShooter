using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject hitPrefab;
    public int damage;
    public int laserRange = 17;
    Vector2 initPos;

    void Start()
    {
        rb.velocity = transform.up * speed;
        initPos = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(initPos, transform.position) > laserRange)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Laser")
            goto a;
        
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

        a:;
    }
}
