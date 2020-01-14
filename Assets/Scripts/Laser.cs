using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    float time = 0;
    public Rigidbody2D rb;
    public GameObject hitPrefab;
    public int damage;
    public int laserLife;
    public GameObject gun;

    void Start()
    {
        if (gun == null)
            gun = GameAssets.i.player;
        Vector3 gunVelocity = gun.GetComponent<Rigidbody2D>().velocity;
        rb.velocity = transform.up * speed + gunVelocity;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > laserLife)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Laser") || GameAssets.i.IsPowerUp(hitInfo))
            goto a;
        
        Vector2 hitPosition = transform.position;
        
        if(hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        if(hitInfo.CompareTag("Player"))
        {
            Player player = GameAssets.i.player.GetComponent<Player>();
            player.TakeDamage(damage);
        }
        Destroy(gameObject);

        Instantiate(hitPrefab, hitPosition, Quaternion.identity);

        a:;
    }
}
