using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject yellowPillPrefab;
    public GameObject bluePillPrefab;
    public GameObject greenPillPrefab;
    Vector2 enemyPos;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    int starCount;
    int mapSize;
    public float yellowDelay, blueDelay, greenDelay;
    float yellowTime, blueTime, greenTime;
    Transform playerPos;
    public GameObject pillSpawn;
    public GameObject cursor;
    public Camera cam;

    void Start()
    {
        playerPos = GameAssets.i.player.transform;

        yellowTime = blueTime = greenTime = 0;

        mapSize = GameAssets.i.mapSize;
        starCount = mapSize / 2;

        transform.localScale = new Vector2(mapSize, mapSize);

        for (int i = 0; i < starCount; i++)
        {
            Instantiate(star1, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
            Instantiate(star2, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
            Instantiate(star3, new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize)), Quaternion.identity);
        }

        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = target;

        yellowTime += Time.deltaTime;
        blueTime += Time.deltaTime;
        greenTime += Time.deltaTime;

        if (GameObject.Find("Enemy(Clone)") == null)
        {
            int positive = Random.Range(1, 3);
            if(positive == 1)
                enemyPos = new Vector2(Random.Range(-mapSize - 5, -mapSize), Random.Range(-mapSize - 5, -mapSize));
            else
                enemyPos = new Vector2(Random.Range(mapSize, mapSize + 5), Random.Range(mapSize, mapSize + 5));
            Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        }

        Vector2 pillPos;
        if (yellowTime >= yellowDelay)
        {
            do
            {
                pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(pillPos, playerPos.position) <= 0.7f);

            Instantiate(pillSpawn, pillPos, Quaternion.identity);
            Instantiate(yellowPillPrefab, pillPos, Quaternion.identity);
            yellowTime -= yellowDelay;
        }

        if (blueTime >= blueDelay)
        {
            do
            {
                pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(pillPos, playerPos.position) <= 0.7f);

            Instantiate(pillSpawn, pillPos, Quaternion.identity);
            Instantiate(bluePillPrefab, pillPos, Quaternion.identity);
            blueTime -= blueDelay;
        }

        if (greenTime >= greenDelay)
        {
            do
            {
                pillPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(pillPos, playerPos.position) <= 0.7f);

            Instantiate(pillSpawn, pillPos, Quaternion.identity);
            Instantiate(greenPillPrefab, pillPos, Quaternion.identity);
            greenTime -= greenDelay;
        }

    }

}
