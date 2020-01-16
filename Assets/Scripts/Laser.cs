using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    float time = 0;
    public GameObject hitPrefab;
    [HideInInspector] public int damage;
    public int laserLife;
    public string goThrough;

    void Start()
    {
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
        if (hitInfo.CompareTag("Laser") || GameAssets.i.IsPowerUp(hitInfo) || hitInfo.CompareTag(goThrough))
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
