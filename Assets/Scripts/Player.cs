﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    int mapSize;
    public float force;
    float moveX, moveY;
    [HideInInspector] public float finalHealth, finalShield;
    [HideInInspector] public bool adrenalineRanOut, shieldRanOut;
    float health, shield;
    public int maxHealth, maxShield;
    public GameObject damage, shieldSprite;
    public Image healthBar, shieldBar, hypedTimer;
    public Camera cam;
    Rigidbody2D rb;
    Vector2 mousePos;
    Vector2 moveDir;
    Vector2 pos;
    public GameObject playerDeath;

    [HideInInspector] public enum ShieldState
    { disabled, power1, power2, power3 }
    [HideInInspector] public ShieldState state;

    [HideInInspector] public float maxHyperTime, hyperTime, finalHyperTime;

    CameraShake shake;

    void Start()
    {
        
        finalHealth = health = maxHealth;
        mapSize = GameAssets.i.mapSize;
        rb = GetComponent<Rigidbody2D>();

        adrenalineRanOut = shieldRanOut = true;
        maxHyperTime = 1;
        hyperTime = finalHyperTime = 0;

        shake = GameObject.FindGameObjectWithTag("CamShake").GetComponent<CameraShake>();

    }

    void Update()
    {
        
        if (finalHyperTime <= 0)
            Movement(1, 2, 1);
        else
        {
            Movement(3, 5, 2);
            finalHyperTime -= Time.deltaTime;
            if (finalHyperTime <= 1f && !adrenalineRanOut)
            {
                FindObjectOfType<AudioManager>().Play("AdrenalineRanOut");
                adrenalineRanOut = true;
            }
        }

        if (Mathf.Abs(hyperTime - finalHyperTime) > 0f)
            hyperTime -= Time.deltaTime * (hyperTime - finalHyperTime) * 5;
        hypedTimer.fillAmount = hyperTime / maxHyperTime;
        
        
        HealthHandle();
        ShieldHandle();

    }

    void ShieldHandle()
    {
        if(shield <= maxShield && shield >= 0.667 * maxShield)
        {
            shieldSprite.GetComponent<SpriteRenderer>().sprite = GameAssets.i.shield3;
            state = ShieldState.power3;
        }
        else if (shield <= 0.667 * maxShield && shield >= 0.333 * maxShield)
        {
            shieldSprite.GetComponent<SpriteRenderer>().sprite = GameAssets.i.shield2;
            state = ShieldState.power2;
        }

        else if (shield <= 0.333 * maxShield && shield > 0)
        {
            shieldSprite.GetComponent<SpriteRenderer>().sprite = GameAssets.i.shield1;
            state = ShieldState.power1;
        }
        else
        {
            shield = 0;
            shieldSprite.GetComponent<SpriteRenderer>().sprite = null;
            state = ShieldState.disabled;
        }

        shieldSprite.transform.position = transform.position;

        if (Mathf.Abs(shield - finalShield) > 0.5f)
            shield -= Time.deltaTime * (shield - finalShield) * 10f;
        shieldBar.fillAmount = shield / maxShield;

    }
    void HealthHandle()
    {

        if (health <= 0.75 * maxHealth && health >= 0.5 * maxHealth)
        {
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage1;
            healthBar.color = GameAssets.i.GetColorFromHex("ffff00");
        }

        else if (health <= 0.5 * maxHealth && health >= 0.25 * maxHealth)
        {
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage2;
            healthBar.color = GameAssets.i.GetColorFromHex("FF6E00");
        }
        else if (health <= 0.25 * maxHealth)
        {
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage3;
            healthBar.color = GameAssets.i.GetColorFromHex("ff0000");
        }
        else
        {
            damage.GetComponent<SpriteRenderer>().sprite = null;
            healthBar.color = GameAssets.i.GetColorFromHex("00ff00");
        }
            
        damage.transform.position = transform.position;

        if (Mathf.Abs(health - finalHealth) > 0.5f)
            health -= Time.deltaTime * (health - finalHealth) * 10f;
        healthBar.fillAmount = health / maxHealth;

        if (health <= 0)
            Die();
        else if (finalHealth > maxHealth)
            finalHealth = maxHealth;
    }

    void Movement(float forceMultiplier, float stoppingDrag, float movingDrag)
    {
        
        if (Input.GetKey(KeyCode.W))
            moveY += 1f;
        if (Input.GetKey(KeyCode.S))
            moveY -= 1f;
        if (Input.GetKey(KeyCode.D))
            moveX += 1f;
        if (Input.GetKey(KeyCode.A))
            moveX -= 1f;

        moveDir = new Vector2(moveX, moveY).normalized;
        rb.AddForce(moveDir * force * forceMultiplier * Time.deltaTime);

        if ((moveX == 0 && moveY == 0) && (Mathf.Abs(rb.velocity.x) >= 0.1 || Mathf.Abs(rb.velocity.x) <= -0.1 || Mathf.Abs(rb.velocity.y) >= 0.1 || Mathf.Abs(rb.velocity.y) <= -0.1))
            rb.drag = stoppingDrag;
        else
            rb.drag = movingDrag;

        moveX = moveY = 0;

        pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -mapSize, mapSize);
        pos.y = Mathf.Clamp(pos.y, -mapSize, mapSize);
        transform.position = pos;

        Look();

    }

    void Look()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void TakeDamage(int damage)
    {
        if (state == ShieldState.disabled)
        {
            finalHealth = health - damage;
            StartCoroutine(GameAssets.i.ChangeColor("ff0000", gameObject, null));
        }
        else
        {
            finalShield = shield - damage;
            if (finalShield <= 0 && !shieldRanOut)
            {
                finalShield = 0;
                FindObjectOfType<AudioManager>().Play("ShieldDown");
                shieldRanOut = true;
            }
        }

            DamagePopup.Create(transform.position, damage);
    }

    void Die()
    {
        Instantiate(playerDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        shake.CamShake("ShakeBig");
    }

}
