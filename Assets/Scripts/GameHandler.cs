using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    Vector2 enemyPos;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    int starCount;
    int mapSize;

    void Start()
    {

        mapSize = GameAssets.i.mapSize;
        starCount = mapSize / 2;

        transform.localScale = new Vector2(mapSize, mapSize);

        for (int i = 0; i < starCount; i++)
        {
            Instantiate(star1, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
            Instantiate(star2, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
            Instantiate(star3, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
        }

    }

    void Update()
    {
        if(GameObject.Find("Enemy(Clone)") == null)
        {
            enemyPos = new Vector2(Random.Range(-12, 12), Random.Range(-12, 12));
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        }
        
    }

}
