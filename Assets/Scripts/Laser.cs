using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    float speed;
    public int dir;
    float time = 0;
    public GameObject hitPrefab;
    [HideInInspector] public int damage;
    public int laserLife;
    public string goThrough;

    void Start()
    {
        speed = dir * GameAssets.i.laserSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > laserLife)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(! (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Player")) )
            goto a;
        
        Vector2 hitPosition = transform.position;
        
        if(hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy)
                enemy.TakeDamage(damage);
            else
            {
                EnemyHealer enemyHealer= hitInfo.GetComponent<EnemyHealer>();
                enemyHealer.TakeDamage(damage);
            }

        }
        if(hitInfo.CompareTag("Player"))
        {
            Player player = hitInfo.GetComponent<Player>();
            player.TakeDamage(damage);
        }
        Destroy(gameObject);

        Instantiate(hitPrefab, hitPosition, Quaternion.identity);

        a:;
    }
}
