using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public Transform gun;
    public GameObject laserPrefab;
    public int damage;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetButton("Fire1") && time >= 0.1f)
        {
            shootLaser();
            time = 0;
        }
    }
    public void shootLaser(){

        GameObject laser = Instantiate(laserPrefab, gun.position, gun.rotation);
        laser.GetComponent<Laser>().damage = damage;        
    }
}
