using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    GameObject player;
    float shield, maxShield;
    public float shielding;
    public float lifeSpan;
    float time;
    public GameObject pickUp, vanish;
    
    private void Start()
    {
        player = GameAssets.i.player;
        maxShield = player.GetComponent<Player>().maxShield;
        time = 0;
    }
    void Update()
    {
        if (player)
        {
            shield = player.GetComponent<Player>().finalShield;

            time += Time.deltaTime;
            if (time >= lifeSpan)
            {
                Instantiate(vanish, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickUp();
            FindObjectOfType<AudioManager>().Play("ShieldUp");
        }
    }

    void PickUp()
    {
        if (player)
        {
            shield += maxShield * shielding;
            if (shield > maxShield)
                shield = maxShield;
            player.GetComponent<Player>().finalShield = shield;
            player.GetComponent<Player>().shieldRanOut = false;
            Instantiate(pickUp, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
