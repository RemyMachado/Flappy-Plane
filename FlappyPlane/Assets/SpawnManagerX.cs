using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject player;

    [System.Serializable]
    public struct PowerUp
    {
        public GameObject prefab;
        public float dropChance; // [0, 1]
    }

    public PowerUp[] powerUps;
    public GameObject obstaclePrefab;
    public float minDistance;
    public float maxDistance;
    public float renderDistance;
    public float minAltitude;
    public float maxAltitude;
    private List<GameObject> obstacles = new List<GameObject>();
    private List<GameObject> pickUps = new List<GameObject>();

    public void Update()
    {
        if (player && player.transform.position.z > renderDistance * (obstacles.Count - 1) - 1)
        {
            SpawnPowerUps();
            SpawnObstacle();
        }
    }

    private void SpawnPowerUps()
    {
        float dropPercentage = Random.value;

        for (uint i = 0; i < powerUps.Length; ++i)
        {
            for (uint spawnQuantity = 0; spawnQuantity < 3; spawnQuantity++)
            {
                if (powerUps[i].dropChance >= dropPercentage)
                {
                    GameObject powerUpPrefab = powerUps[i].prefab;
                    GameObject pickUp = Instantiate(powerUpPrefab, GenerateSpawnPos(powerUpPrefab, false),
                        powerUpPrefab.transform.rotation);
                    pickUps.Add(pickUp);
                }
            }
        }
    }

    private Vector3 GenerateSpawnPos(GameObject prefab, bool isObstacle)
    {
        float lastItemZ = 0;

        if (isObstacle)
        {
            if (obstacles.Count > 0)
            {
                lastItemZ = obstacles[obstacles.Count - 1].transform.position.z;
            }
        }
        else
        {
            if (pickUps.Count > 0)
            {
                lastItemZ = pickUps[pickUps.Count - 1].transform.position.z;
            }
        }

        float z = Random.Range(lastItemZ + minDistance, lastItemZ + maxDistance);
        float y = Random.Range(minAltitude, maxAltitude);

        Vector3 randomPosition = new Vector3(0, y, z);

        return randomPosition;
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPos = GenerateSpawnPos(obstaclePrefab, true);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        obstacles.Add(obstacle);
    }
}