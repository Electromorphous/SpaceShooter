using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform gun;
    public GameObject laserPrefab;
    float time;
    public float shootDelay;
    [HideInInspector] public bool shoot = false;

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

        Instantiate(laserPrefab, gun.position, gun.rotation);

    }
}
