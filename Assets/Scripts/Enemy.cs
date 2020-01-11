using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    public float force;
    float moveX, moveY;
    float health;
    public float maxHealth = 100;
    public Image healthBar;
    GameObject target;
    public int attackRange;
    public GameObject enemyGun;

    void Start()
    {
        health = maxHealth;
        target = GameAssets.i.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            Movement();
            enemyGun.GetComponent<EnemyGun>().shoot = false;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            enemyGun.GetComponent<EnemyGun>().shoot = true;
        }

        Vector2 lookDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;

        healthBar.fillAmount = health / maxHealth;
    }

    void Movement()
    {
        if (target.transform.position.y > transform.position.y)
            moveY += +1f;
        if (target.transform.position.y < transform.position.y)
            moveY += -1f;
        if (target.transform.position.x > transform.position.x)
            moveX += +1f;
        if (target.transform.position.x < transform.position.x)
            moveX += -1f;

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.AddForce(moveDir * force * Time.deltaTime);

        if ((moveX == 0 && moveY == 0) && (Mathf.Abs(rb.velocity.x) >= 0.1 || Mathf.Abs(rb.velocity.x) <= -0.1 || Mathf.Abs(rb.velocity.y) >= 0.1 || Mathf.Abs(rb.velocity.y) <= -0.1))
            rb.drag = 2;
        else
            rb.drag = 1;

        moveX = moveY = 0;

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
