using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : MonoBehaviour
{
    GameObject player;
    float time;
    public float lastingTime;
    public float lifeSpan;
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
            FindObjectOfType<AudioManager>().Play("AdrenalinePickUp");
        }
    }

    void PickUp()
    {
        if (player)
        {
            player.GetComponent<Player>().adrenalineRanOut = false;
            player.GetComponent<Player>().maxHyperTime = lastingTime;
            player.GetComponent<Player>().finalHyperTime = lastingTime;
            StartCoroutine(GameAssets.i.ChangeColor("ffff00", player, gameObject));
            Instantiate(pickUp, transform.position, transform.rotation);
        }
    }
}

