using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealer : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    float moveX, moveY;
    float health;
    public float maxHealth;
    public Image healthBar;
    GameObject target = null;
    public float healRange;
    public GameObject enemyGun;
    float laserSpeed;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        laserSpeed = GameAssets.i.laserSpeed;
    }

    void Update()
    {
        
        target = GetClosestObject("Enemy");
        if (target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > healRange)
            {
                Movement();
                enemyGun.GetComponent<EnemyGun>().shoot = false;
            }
            else
            {
                rb.drag = 3;
                enemyGun.GetComponent<EnemyGun>().shoot = true;
            }

            PredictLook();

        }

        if(!target)
            enemyGun.GetComponent<EnemyGun>().shoot = false;


        healthBar.fillAmount = health / maxHealth;

    }

    GameObject GetClosestObject(string tagName)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagName);
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in objectsWithTag)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            Enemy enemy = potentialTarget.GetComponent<Enemy>();
            EnemyHealer enemyHealer = potentialTarget.GetComponent<EnemyHealer>();
            if ((potentialTarget != gameObject) && (enemy && enemy.health < enemy.maxHealth || enemyHealer && enemyHealer.health < enemyHealer.maxHealth))
            {
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }
        return bestTarget;
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

        if ((moveX == 0 && moveY == 0) && (Mathf.Abs(rb.velocity.x) >= 0.1 || Mathf.Abs(rb.velocity.y) >= 0.1))
            rb.drag = 2;
        else
            rb.drag = 1;

        moveX = moveY = 0;

    }

    void PredictLook()
    {
        Vector2 initialPos = target.transform.position;
        Vector3 finalPos = initialPos + (target.GetComponent<Rigidbody2D>().velocity) * (Vector2.Distance(transform.position, initialPos)) / laserSpeed;
        Vector2 lookDir = finalPos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        DamagePopup.Create(transform.position, damage);

        if (health <= 0)
        {
            Die();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
