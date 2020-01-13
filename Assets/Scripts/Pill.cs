using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    GameObject player;
    float health, maxHealth;
    public float healing;
    public float lifeSpan;
    float time;
    public GameObject pickUp, vanish;
    

    private void Start()
    {
        player = GameAssets.i.player;
        maxHealth = player.GetComponent<Player>().maxHealth;
        time = 0;
    }
    void Update()
    {
        float angle = transform.eulerAngles.z;
        angle += 333f * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, angle);

        health = player.GetComponent<Player>().finalHealth;

        time += Time.deltaTime;
        if (time >= lifeSpan)
        {
            Instantiate(vanish, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        health += maxHealth * healing;
        if (health > maxHealth)
            health = maxHealth;
        player.GetComponent<Player>().finalHealth = health;

        Instantiate(pickUp, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
