using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject yellowPillPrefab;
    public GameObject bluePillPrefab;
    public GameObject greenPillPrefab;
    GameObject[] getCount;
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
            int positive = Random.Range(1, 3);
            if(positive == 1)
                enemyPos = new Vector2(Random.Range(-mapSize - 5, -mapSize), Random.Range(-mapSize - 5, -mapSize));
            else
                enemyPos = new Vector2(Random.Range(mapSize, mapSize + 5), Random.Range(mapSize, mapSize + 5));
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        }

        getCount = GameObject.FindGameObjectsWithTag("yellowPill");
        if (getCount.Length <= 2)
        {
            Vector2 pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            Instantiate(yellowPillPrefab, pillPos, Quaternion.identity);
        }

        getCount = GameObject.FindGameObjectsWithTag("bluePill");
        if (getCount.Length <= 1)
        {
            Vector2 pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            Instantiate(bluePillPrefab, pillPos, Quaternion.identity);
        }

        getCount = GameObject.FindGameObjectsWithTag("greenPill");
        if (getCount.Length == 0)
        {
            Vector2 pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            Instantiate(greenPillPrefab, pillPos, Quaternion.identity);
        }

    }

}
