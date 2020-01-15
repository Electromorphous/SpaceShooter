using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    Vector2 enemyspawnPos;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    int starCount;
    int mapSize;
    Transform playerspawnPos;
    public GameObject cursor;
    public Camera cam;

    public GameObject yellowPillPrefab, bluePillPrefab, greenPillPrefab, shieldPrefab, adrenalinePrefab;
    public float yellowDelay, blueDelay, greenDelay, shieldDelay, adrenalineDelay;
    float yellowTime, blueTime, greenTime, shieldTime, adrenalineTime;
    public GameObject pillSpawn, shieldSpawn, adrenalineSpawn;

    void Start()
    {
        playerspawnPos = GameAssets.i.player.transform;

        yellowTime = blueTime = greenTime = shieldTime = 0;

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
        shieldTime += Time.deltaTime;
        adrenalineTime += Time.deltaTime;

        if (GameObject.Find("Enemy(Clone)") == null)
        {
            int positive = Random.Range(1, 3);
            if (positive == 1)
                enemyspawnPos = new Vector2(Random.Range(-mapSize - 5, -mapSize), Random.Range(-mapSize - 5, -mapSize));
            else
                enemyspawnPos = new Vector2(Random.Range(mapSize, mapSize + 5), Random.Range(mapSize, mapSize + 5));
            Instantiate(enemyPrefab, enemyspawnPos, Quaternion.identity);
        }

        Vector2 spawnPos;
        if (yellowTime >= yellowDelay)
        {
            do
            {
                spawnPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(spawnPos, playerspawnPos.position) <= 0.7f);

            Instantiate(pillSpawn, spawnPos, Quaternion.identity);
            Instantiate(yellowPillPrefab, spawnPos, Quaternion.identity);
            yellowTime -= yellowDelay;
        }

        if (blueTime >= blueDelay)
        {
            do
            {
                spawnPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(spawnPos, playerspawnPos.position) <= 0.7f);

            Instantiate(pillSpawn, spawnPos, Quaternion.identity);
            Instantiate(bluePillPrefab, spawnPos, Quaternion.identity);
            blueTime -= blueDelay;
        }

        if (greenTime >= greenDelay)
        {
            do
            {
                spawnPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(spawnPos, playerspawnPos.position) <= 0.7f);

            Instantiate(pillSpawn, spawnPos, Quaternion.identity);
            Instantiate(greenPillPrefab, spawnPos, Quaternion.identity);
            greenTime -= greenDelay;
        }

        if (shieldTime >= shieldDelay)
        {
            do
            {
                spawnPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(spawnPos, playerspawnPos.position) <= 0.7f);

            Instantiate(shieldSpawn, spawnPos, Quaternion.identity);
            Instantiate(shieldPrefab, spawnPos, Quaternion.identity);
            shieldTime -= shieldDelay;
        }

        if (adrenalineTime >= adrenalineDelay)
        {
            do
            {
                spawnPos = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            } while (Vector2.Distance(spawnPos, playerspawnPos.position) <= 0.7f);

            Instantiate(adrenalineSpawn, spawnPos, Quaternion.identity);
            Instantiate(adrenalinePrefab, spawnPos, Quaternion.identity);
            adrenalineTime -= adrenalineDelay;
        }
    }

}
