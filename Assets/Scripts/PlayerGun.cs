using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public Transform gun;
    public GameObject laserPrefab;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shootLaser();
            
        }
    }
    public void shootLaser(){

        Instantiate(laserPrefab, gun.position, gun.rotation);
        
        
    }
}
