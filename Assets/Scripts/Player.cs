using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    int mapSize;
    public float force;
    float moveX, moveY;
    [HideInInspector] public float finalHealth;
    float health;
    public int maxHealth;
    public GameObject damage;
    public Image healthBar;
    public Camera cam;
    Rigidbody2D rb;
    Vector2 mousePos;
    Vector2 moveDir;
    Vector2 pos;

    void Start()
    {
        finalHealth = health = maxHealth;
        mapSize = GameAssets.i.mapSize;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        HealthHandle();
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
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.W))
            moveY += +1f;
        if (Input.GetKey(KeyCode.S))
            moveY += -1f;
        if (Input.GetKey(KeyCode.D))
            moveX += +1f;
        if (Input.GetKey(KeyCode.A))
            moveX += -1f;

        moveDir = new Vector2(moveX, moveY).normalized;
        rb.AddForce(moveDir * force * Time.deltaTime);

        if ((moveX == 0 && moveY == 0) && (Mathf.Abs(rb.velocity.x) >= 0.1 || Mathf.Abs(rb.velocity.x) <= -0.1 || Mathf.Abs(rb.velocity.y) >= 0.1 || Mathf.Abs(rb.velocity.y) <= -0.1))
            rb.drag = 2;
        else
            rb.drag = 1;

        moveX = moveY = 0;

        pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -mapSize, mapSize);
        pos.y = Mathf.Clamp(pos.y, -mapSize, mapSize);
        transform.position = pos;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
    public void TakeDamage(int damage)
    {
        finalHealth = health - damage;

        DamagePopup.Create(transform.position, damage);

    }

    void Die()
    {
        Destroy(gameObject);
        
    }

}
