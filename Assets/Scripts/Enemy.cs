using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    public float force;
    float moveX, moveY;
    float diffMax = 3.7f;
    float health;
    public float maxHealth = 100;
    public Image healthBar;
    Vector2 pos;
    int mapSize;
    GameObject target;

    void Start()
    {
        health = maxHealth;
        target = GameAssets.i.player;
        mapSize = GameAssets.i.mapSize;
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void Update()
    {
        

    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        DamagePopup.Create(transform.position, damage);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
