using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject pairPrefab;
    public float pairSpawnDelay;
    float pairTime;
    public GameObject hexPrefab;
    public float hexSpawnDelay;
    float hexTime;
    public GameObject normPrefab;
    public float normSpawnDelay;
    float normTime;
    public GameObject healerPrefab;
    public float healerSpawnDelay;
    float healerTime;
    public GameObject predictorPrefab;
    public float predictorSpawnDelay;
    float predictorTime;

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

    void Start()
    {

        yellowTime = blueTime = greenTime = shieldTime = pairTime = hexTime = normTime = healerTime = predictorTime = 0;

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
        playerPos = GameAssets.i.player.transform;

        pairTime += Time.deltaTime;
        hexTime += Time.deltaTime;
        normTime += Time.deltaTime;
        healerTime += Time.deltaTime;
        predictorTime += Time.deltaTime;

        yellowTime += Time.deltaTime;
        blueTime += Time.deltaTime;
        greenTime += Time.deltaTime;
        shieldTime += Time.deltaTime;
        adrenalineTime += Time.deltaTime;

        if (pairTime >= pairSpawnDelay)
        {
            Instantiate(pairPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(pairPrefab, RandomSpawnEnemy(), Quaternion.identity);
            pairTime -= pairSpawnDelay;
        }

        if (hexTime >= hexSpawnDelay)
        {
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            Instantiate(hexPrefab, RandomSpawnEnemy(), Quaternion.identity);
            hexTime -= hexSpawnDelay;
        }

        if (normTime >= normSpawnDelay)
        {
            Instantiate(normPrefab, RandomSpawnEnemy(), Quaternion.identity);
            normTime -= normSpawnDelay;
        }

        if (healerTime >= healerSpawnDelay)
        {
            Instantiate(healerPrefab, RandomSpawnEnemy(), Quaternion.identity);
            healerTime -= healerSpawnDelay;
        }

        if (predictorTime >= predictorSpawnDelay)
        {
            Instantiate(predictorPrefab, RandomSpawnEnemy(), Quaternion.identity);
            predictorTime -= predictorSpawnDelay;
        }

        Vector2 spawnPos;
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
