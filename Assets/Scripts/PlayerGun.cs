using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public Transform gun;
    public GameObject laserPrefab;
    public int damage;
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shootLaser();
            
        }
    }
    public void shootLaser(){

        GameObject laser = Instantiate(laserPrefab, gun.position, gun.rotation);
        laser.GetComponent<Laser>().damage = damage;        
    }
}
