﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;
    float moveX, moveY;
    [HideInInspector] public float health;
    public float maxHealth;
    public Image healthBar;
    GameObject target;
    public int attackRange, lookRange;
    public GameObject enemyGun;
    public bool predictor;
    float laserSpeed;
    public GameObject enemyDeath;
    CameraShake shake;
    public int killPoints;

    void Start()
    {
        health = maxHealth;
        target = GameAssets.i.player;
        rb = GetComponent<Rigidbody2D>();
        laserSpeed = GameAssets.i.laserSpeed;

        shake = GameObject.FindGameObjectWithTag("CamShake").GetComponent<CameraShake>();

        StartCoroutine(EnterMap());
        
    }

    IEnumerator EnterMap()
    {
        float i = 0;
        float enterTime = Random.Range(0.5f, 1f);
        while (i < enterTime)
        {
            i += Time.deltaTime;
            Movement();
            yield return null;
        }

    }

    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange && Vector3.Distance(transform.position, target.transform.position) < lookRange)
            {
                Movement();
                enemyGun.GetComponent<EnemyGun>().shoot = false;
            }
            else if(Vector3.Distance(transform.position, target.transform.position) < attackRange)
            {
                rb.drag = 3;
                enemyGun.GetComponent<EnemyGun>().shoot = true;
            }
            if(Vector3.Distance(transform.position, target.transform.position) < lookRange)
            {
                if (!predictor)
                    Look();
                else
                    PredictLook();
            }
        }
        else
            enemyGun.GetComponent<EnemyGun>().shoot = false;
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

        if ((moveX == 0 && moveY == 0) && (Mathf.Abs(rb.velocity.x) >= 0.1 || Mathf.Abs(rb.velocity.y) >= 0.1))
            rb.drag = 2;
        else
            rb.drag = 1;

        moveX = moveY = 0;
    
    }

    void Look()
    {
        Vector2 lookDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
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
        StartCoroutine(GameAssets.i.ChangeColor("ff0000", gameObject, null));

        DamagePopup.Create(transform.position, damage);

        if (health <= 0)
        {
            Die();
        }
        else if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
        FindObjectOfType<GameHandler>().finalScore += killPoints;
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        shake.CamShake("ShakeSmall");
    }
}
