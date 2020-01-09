using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform gun;
    public GameObject laserPrefab;
    private float time;
    public float delay = 0.5f;

    private void Update()
    {

        time += Time.deltaTime;
        
        if (time >= delay)
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
