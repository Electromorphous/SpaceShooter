using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public Transform gun;
    public GameObject laserPrefab;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            shootLaser();
        }
    }
    public void shootLaser(){

        Instantiate(laserPrefab, gun.position, gun.rotation);

    }
}
