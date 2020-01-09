using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector2 enemyPos;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    private int starCount = 23;
    public GameObject player;
    public GameObject mainCamera;
    private Vector3 camPos;
    private float mapSize;

    private void Start()
    {

        mapSize = transform.localScale.x;

        for (int i = 0; i < starCount; i++)
        {
            Instantiate(star1, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-17, 17)), Quaternion.identity);
            Instantiate(star2, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-17, 17)), Quaternion.identity);
            Instantiate(star3, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-17, 17)), Quaternion.identity);
        }

    }

    void Update()
    {
        if(GameObject.Find("Enemy(Clone)") == null)
        {
            enemyPos = new Vector2(Random.Range(-12, 12), Random.Range(-12, 12));
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        }

        camPos = player.transform.position;
        camPos.z = -10;

        camPos.x = Mathf.Clamp(camPos.x, -mapSize , mapSize );
        camPos.y = Mathf.Clamp(camPos.y, -mapSize , mapSize );

        mainCamera.transform.position = camPos;
        
    }

}
