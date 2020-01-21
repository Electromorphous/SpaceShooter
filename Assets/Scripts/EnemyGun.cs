using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject laserPrefab;
    float time;
    public float shootDelay;
    [HideInInspector] public bool shoot = false;
    public int damage;
    public bool pulse;

    void Update()
    {
        time += Time.deltaTime;
        
        if (time >= shootDelay && shoot == true)
        {
            if (!pulse)
                ShootLaser();
            else
                StartCoroutine(PulseLaser());
            time = 0;
        }
    }
    void ShootLaser()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("EnemyLaser");
        laser.GetComponent<Laser>().damage = damage;
    }
    IEnumerator PulseLaser()
    {
        float delayCounter = 0;
        int i = 3;
        while(i > 0)
        {
            delayCounter += Time.deltaTime;

            if(delayCounter >= 0.05f)
            {
                GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("EnemyLaser");
                laser.GetComponent<Laser>().damage = damage;
                delayCounter = 0;
                i--;
            }

            yield return null;
        }
    }
}
