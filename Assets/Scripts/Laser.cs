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
    Vector2 initPos;
    public GameObject thing;

    void Start()
    {
        if (thing == null)
            thing = GameAssets.i.player;
        Vector3 gunVelocity = thing.GetComponent<Rigidbody2D>().velocity;
        rb.velocity = transform.up * speed + gunVelocity;
        initPos = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > laserLife)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Laser"))   // || hitInfo.CompareTag("bluePill") || hitInfo.CompareTag("greenPill") || hitInfo.CompareTag("yellowPill") || hitInfo.CompareTag("Shield") || hitInfo.CompareTag("Adrenaline"))
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
