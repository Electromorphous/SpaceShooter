using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb;
    private float upSpeed;
    private float rightSpeed;
    private float speedMax = 4.2f;
    private float diffMax = 3.7f;
    private float health;
    public float maxHealth = 100;
    public Image healthBar;
    private Vector3 shipPos;
    private float mapSize = 17f;
    private GameObject player;

    void Start()
    {
        upSpeed = speedMax;
        rightSpeed = speedMax;
        health = maxHealth;
        player = GameAssets.i.player;
    }

    void Update()
    {
        rb.velocity = new Vector2 (rightSpeed, upSpeed);

        shipPos = transform.position;
        shipPos.x = Mathf.Clamp(shipPos.x, -mapSize + 5, mapSize - 5);
        shipPos.y = Mathf.Clamp(shipPos.y, -mapSize + 5, mapSize - 5);
        transform.position = shipPos;

        healthBar.fillAmount = health / maxHealth;

        float diffX = transform.position.x - player.transform.position.x;
        float diffY = transform.position.y - player.transform.position.y;
        
        if (diffX > diffMax || diffX < -diffMax)
        {
            if (transform.position.x < player.transform.position.x && rightSpeed <= 0)
                rightSpeed = speedMax;
            else if (transform.position.x > player.transform.position.x && rightSpeed >= 0)
                rightSpeed = -speedMax;
        }
        else
            rightSpeed = 0;

        if (diffY > diffMax || diffY < -diffMax)
        {
            if (transform.position.y < player.transform.position.y && upSpeed <= 0)
                upSpeed = speedMax;
            else if (transform.position.y > player.transform.position.y && upSpeed >= 0)
                upSpeed = -speedMax;
        }
        else
            upSpeed = 0;

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
