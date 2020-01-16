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

    void Update()
    {

        time += Time.deltaTime;
        
        if (time >= shootDelay && shoot == true)
        {
            shootLaser();
            time = 0;
        }
    }
    public void shootLaser()
    {

        GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
        laser.GetComponent<Laser>().damage = damage;
    }
}
