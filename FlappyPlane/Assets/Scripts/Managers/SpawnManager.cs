using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [System.Serializable]
        public struct PowerUp
        {
            public GameObject prefab;
            public float dropChance; // [0, 1]
        }

        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public GameObject enemyAxisPrefab;
        public GameObject enemySpeedIncreasePrefab;
        public GameObject enemyGrowPrefab;
        public PowerUp[] powerUps;
        private WaveManager _waveManager;
        private Ground _ground;
        private readonly Vector3 _playerStartingPosition = new Vector3(0, 0.5f, 0);

        private void Awake()
        {
            Instantiate(playerPrefab, _playerStartingPosition, playerPrefab.transform.rotation);
        }

        private void Start()
        {
            _ground = GameObject.Find("Ground").GetComponent<Ground>();
            _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            _waveManager.OnNewWave += SpawnEnemyWave;
            _waveManager.OnNewWave += SpawnPowerUps;
        }

        private void SpawnPowerUps(uint wave)
        {
            float dropPercentage = Random.value;

            for (uint i = 0; i < powerUps.Length; ++i)
            {
                if (powerUps[i].dropChance >= dropPercentage)
                {
                    GameObject powerUpPrefab = powerUps[i].prefab;
                    Instantiate(powerUpPrefab, GenerateSpawnPos(powerUpPrefab), powerUpPrefab.transform.rotation);
                }
            }
        }

        private Vector3 GenerateSpawnPos(GameObject prefab)
        {
            float groundSideWidth = _ground.GetCurrentWidth() / 2;
            Vector3 randomPosition = groundSideWidth * Random.value * Random.onUnitSphere;
            
            randomPosition.y = prefab.transform.position.y;
                    
            return randomPosition;
        }

        private void SpawnEnemyWave(uint wave)
        {
            if (wave < 10)
            {
                for (uint i = 0; i < wave; ++i)
                {
                    Vector3 spawnPos = GenerateSpawnPos(enemyPrefab);
                    Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
                }
            }
            else if (wave < 20)
            {
                for (uint i = 0; i < wave - 9; ++i)
                {
                    GameObject chosenPrefab;

                    if (i % 2 == 0)
                    {
                        chosenPrefab = enemyAxisPrefab;
                    }
                    else
                    {
                        chosenPrefab = enemyPrefab;
                    }

                    Vector3 spawnPos = GenerateSpawnPos(chosenPrefab);
                    Instantiate(chosenPrefab, spawnPos, chosenPrefab.transform.rotation);
                }
            }
            else if (wave < 30)
            {
                for (uint i = 0; i < wave - 19; ++i)
                {
                    GameObject chosenPrefab;

                    if (i % 3 == 0)
                    {
                        chosenPrefab = enemySpeedIncreasePrefab;
                    }
                    else if (i % 2 == 0)
                    {
                        chosenPrefab = enemyAxisPrefab;
                    }
                    else
                    {
                        chosenPrefab = enemyPrefab;
                    }

                    Vector3 spawnPos = GenerateSpawnPos(chosenPrefab);
                    Instantiate(chosenPrefab, spawnPos, chosenPrefab.transform.rotation);
                }
            }
            else if (wave >= 30)
            {
                for (uint i = 0; i < wave - 29; ++i)
                {
                    GameObject chosenPrefab;

                    if (i % 4 == 0)
                    {
                        chosenPrefab = enemyGrowPrefab;
                    }
                    else if (i % 3 == 0)
                    {
                        chosenPrefab = enemySpeedIncreasePrefab;
                    }
                    else if (i % 2 == 0)
                    {
                        chosenPrefab = enemyAxisPrefab;
                    }
                    else
                    {
                        chosenPrefab = enemyPrefab;
                    }

                    Vector3 spawnPos = GenerateSpawnPos(chosenPrefab);
                    Instantiate(chosenPrefab, spawnPos, chosenPrefab.transform.rotation);
                }
            }
        }
    }
}