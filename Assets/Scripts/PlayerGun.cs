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
            ShootLaser();
            time = 0;
        }
    }
    public void ShootLaser(){

        GameObject laser = Instantiate(laserPrefab, gun.position, gun.rotation);
        FindObjectOfType<AudioManager>().Play("PlayerLaser");
        laser.GetComponent<Laser>().damage = damage;        
    }
}
