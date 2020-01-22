using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    float difficulty = 0;

    public GameObject pairPrefab;
    float pairCount;
    public GameObject hexPrefab;
    float hexCount;
    public GameObject normPrefab;
    float normCount;
    public GameObject healerPrefab;
    float healerCount;
    public GameObject predictorPrefab;
    float predictorCount;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    int starCount;
    int mapSize;
    Transform playerPos;
    public GameObject cursor;
    public Camera cam;

    public GameObject yellowPillPrefab, bluePillPrefab, greenPillPrefab, shieldPrefab, adrenalinePrefab;
    public float yellowDelay, blueDelay, greenDelay, shieldDelay, adrenalineDelay;
    float yellowTime, blueTime, greenTime, shieldTime, adrenalineTime;
    public GameObject pillSpawn, shieldSpawn, adrenalineSpawn;

    GameObject player;

    void Start()
    {
        player = GameAssets.i.player;

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
        if (player)
        {

            difficulty = player.GetComponent<Player>().finalScore / 222f;

            Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);

            cursor.transform.position = target;

            playerPos = GameAssets.i.player.transform;

            pairCount = difficulty + 2;
            hexCount = difficulty + 2;
            normCount = difficulty + 1;
            healerCount = difficulty;
            predictorCount = difficulty;

            yellowTime += Time.deltaTime;
            blueTime += Time.deltaTime;
            greenTime += Time.deltaTime;
            shieldTime += Time.deltaTime;
            adrenalineTime += Time.deltaTime;

            Vector2 spawnPos;

            if (GameObject.FindGameObjectsWithTag("Pair").GetLength(0) < pairCount)
            {
                spawnPos = RandomSpawnEnemy();
                Instantiate(pairPrefab, spawnPos, Quaternion.identity);
                Instantiate(pairPrefab, spawnPos, Quaternion.identity);
            }

            if (GameObject.FindGameObjectsWithTag("HexBros").GetLength(0) < hexCount)
            {
                spawnPos = RandomSpawnEnemy();
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
                Instantiate(hexPrefab, spawnPos, Quaternion.identity);
            }

            if (GameObject.FindGameObjectsWithTag("Norm").GetLength(0) < normCount)
            {
                Instantiate(normPrefab, RandomSpawnEnemy(), Quaternion.identity);
            }

            if (GameObject.FindGameObjectsWithTag("Healer").GetLength(0) < healerCount)
            {
                Instantiate(healerPrefab, RandomSpawnEnemy(), Quaternion.identity);
            }

            if (GameObject.FindGameObjectsWithTag("Predictor").GetLength(0) < predictorCount)
            {
                Instantiate(predictorPrefab, RandomSpawnEnemy(), Quaternion.identity);
            }

            if (yellowTime >= yellowDelay)
            {
                do
                {
                    spawnPos = RandomSpawnPower();
                } while (Vector2.Distance(spawnPos, playerPos.position) <= 0.7f);

                Instantiate(pillSpawn, spawnPos, Quaternion.identity);
                Instantiate(yellowPillPrefab, spawnPos, Quaternion.identity);
                yellowTime -= yellowDelay;
            }

            if (blueTime >= blueDelay)
            {
                do
                {
                    spawnPos = RandomSpawnPower();
                } while (Vector2.Distance(spawnPos, playerPos.position) <= 0.7f);

                Instantiate(pillSpawn, spawnPos, Quaternion.identity);
                Instantiate(bluePillPrefab, spawnPos, Quaternion.identity);
                blueTime -= blueDelay;
            }

            if (greenTime >= greenDelay)
            {
                do
                {
                    spawnPos = RandomSpawnPower();
                } while (Vector2.Distance(spawnPos, playerPos.position) <= 0.7f);

                Instantiate(pillSpawn, spawnPos, Quaternion.identity);
                Instantiate(greenPillPrefab, spawnPos, Quaternion.identity);
                greenTime -= greenDelay;
            }

            if (shieldTime >= shieldDelay)
            {
                do
                {
                    spawnPos = RandomSpawnPower();
                } while (Vector2.Distance(spawnPos, playerPos.position) <= 0.7f);

                Instantiate(shieldSpawn, spawnPos, Quaternion.identity);
                Instantiate(shieldPrefab, spawnPos, Quaternion.identity);
                shieldTime -= shieldDelay;
            }

            if (adrenalineTime >= adrenalineDelay)
            {
                do
                {
                    spawnPos = RandomSpawnPower();
                } while (Vector2.Distance(spawnPos, playerPos.position) <= 0.7f);

                Instantiate(adrenalineSpawn, spawnPos, Quaternion.identity);
                Instantiate(adrenalinePrefab, spawnPos, Quaternion.identity);
                adrenalineTime -= adrenalineDelay;
            }
        }
        else
            Cursor.visible = true;
    }

    Vector2 RandomSpawnEnemy()
    {
        Vector2 spawnPos;
        int dir = Random.Range(1,5);
        if (dir == 1)
            spawnPos = new Vector2(Random.Range(-mapSize - 5, -mapSize), Random.Range(-mapSize - 5, mapSize + 5));
        else if (dir == 2)
            spawnPos = new Vector2(Random.Range(-mapSize - 5, mapSize + 5), Random.Range(mapSize, mapSize + 5));
        else if (dir == 3)
            spawnPos = new Vector2(Random.Range(mapSize, mapSize + 5), Random.Range(-mapSize - 5, mapSize + 5));
        else
            spawnPos = new Vector2(Random.Range(-mapSize - 5, mapSize + 5), Random.Range(-mapSize - 5, -mapSize));

        return spawnPos;
    }
    Vector2 RandomSpawnPower()
    {
        return new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
    }
}
