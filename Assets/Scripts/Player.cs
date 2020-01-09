using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Vector2 shipPosition;
    private float horizAxis;
    private float vertAxis;
    public float mapSize = 17;
    public float speed = 4.2f;
    private float health;
    public int maxHealth = 777;
    public GameObject damage;
    public Image healthBar;

    void Start(){
        shipPosition = new Vector2(0,0);
        health = maxHealth;
    }

    void Update(){

        horizAxis = Input.GetAxis("Horizontal");
        vertAxis = Input.GetAxis("Vertical");
        
        if(vertAxis != 0)
            shipPosition.y += vertAxis * speed * Time.deltaTime;
        if(horizAxis !=  0)
            shipPosition.x += horizAxis * speed * Time.deltaTime;

        shipPosition.x = Mathf.Clamp(shipPosition.x, -mapSize + 5, mapSize - 5);
        shipPosition.y = Mathf.Clamp(shipPosition.y, -mapSize + 5, mapSize - 5);

        transform.position = new Vector3(shipPosition.x, shipPosition.y);

        if(health <= 0.75 * maxHealth && health >= 0.5 * maxHealth)
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage1;
        else if (health <= 0.5 * maxHealth && health >= 0.25 * maxHealth)
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage2;
        else if (health <= 0.25 * maxHealth)
            damage.GetComponent<SpriteRenderer>().sprite = GameAssets.i.damage3;

        damage.transform.position = transform.position;

        healthBar.fillAmount = health / maxHealth;

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
