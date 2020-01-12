using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    GameObject player;
    float health, maxHealth;
    public float healing;
    private void Start()
    {
        player = GameAssets.i.player;
        maxHealth = player.GetComponent<Player>().maxHealth;
    }
    void Update()
    {
        float angle = transform.eulerAngles.z;
        angle += 333f * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, angle);

        health = player.GetComponent<Player>().finalHealth;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Trigger detected");
        if(other.CompareTag("Player"))
        {
            PickUp();
            Debug.Log("Picked Up");
        }
    }

    void PickUp()
    {
        health += maxHealth * healing;
        if (health > maxHealth)
            health = maxHealth;
        player.GetComponent<Player>().finalHealth = health;
        
        Destroy(gameObject);
    }
}
