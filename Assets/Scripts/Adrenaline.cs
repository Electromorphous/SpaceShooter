using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : MonoBehaviour
{
    GameObject player;
    float time;
    public float lastingTime, lifeSpan;
    public GameObject pickUp, vanish;

    void Start()
    {
        time = 0;
        player = GameAssets.i.player;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifeSpan)
        {
            Instantiate(vanish, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        player.GetComponent<Player>().hyper = true;
        Debug.Log("hyper = true");
        Instantiate(pickUp, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

