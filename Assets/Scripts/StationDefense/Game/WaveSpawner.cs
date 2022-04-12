using StationDefense.Enemy;
using StationDefense.Patterns;
using StationDefense.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StationDefense.Game
{
    public class WaveSpawner : Singleton<WaveSpawner>
    {
        public float startDelayTime = 2.0f;
        public float roundLineTime = 2.0f;
        public int startEnemyCount = 1;
        public int enemyIncrement = 1; // per wave
        public int delayTimeIncrement = 0; // per wave
        public float pauseBetweenWaves = 2.0f;
        public float scaler = 4.0f;
        public GameObject enemyPrefab;
        public GameObject[] startPoints;
        public WaveRound waveRound;
        public bool spawn = true;
        private int waveNumber = 1;
        private int enemyCount = 0;
        private int waveEnemies = 0;
        private float delayTime = 0;
        private List<EnemyAgent> enemies = new List<EnemyAgent>();
        private Coroutine _coroutine;

        void Start()
        {
            waveEnemies = startEnemyCount;
            delayTime = startDelayTime;
            _coroutine = StartCoroutine("SpawnEnemy");
        }

        public int GetWaveNumber() => waveNumber;


        public void RemoveFromList(EnemyAgent enemy)
        {
            enemies.Remove(enemy);
        }

        public EnemyAgent GetNearestEnemy(Vector3 position, float maxDistance = Mathf.Infinity)
        {
            float lastDist = maxDistance;
            EnemyAgent result = null;
            foreach (EnemyAgent enemy in enemies)
            {
                float dist = Vector3.Distance(enemy.transform.position, position);
                if (dist < lastDist)
                {
                    lastDist = dist;
                    result = enemy;
                }
            }
            return result;
        }

        public void UpdatePath()
        {
            foreach (EnemyAgent enemy in enemies)
            {
                enemy.UpdateTrack();
            }
        }

        public void StopWaves()
        {
            StopCoroutine(_coroutine);
        }

        IEnumerator SpawnEnemy()
        {
            while (spawn)
            {
                waveRound.ShowRound(waveNumber);
                while (enemyCount < waveEnemies)
                {
                    foreach (GameObject point in startPoints)
                    {
                        GameObject go = Instantiate(enemyPrefab, point.transform.position, Quaternion.identity, transform);
                        EnemyAgent enemy = go.GetComponent<EnemyAgent>();
                        if (enemy != null)
                        {
                            enemy.ScaleHealth(1 + (waveNumber - 1) / scaler);
                            enemies.Add(enemy);
                        }
                    }
                    float waitingTime = delayTime;
                    if (enemyCount == 0)
                    {
                        waitingTime += roundLineTime;
                    }
                    enemyCount++;
                    yield return new WaitForSeconds(waitingTime);
                }

                if (GameManager.instanceExists)
                {
                    GameManager.instance.CurrentWave = waveNumber;
                }

                waveNumber++;
                enemyCount = 0;
                waveEnemies += enemyIncrement;
                delayTime += delayTimeIncrement;

                yield return new WaitForSeconds(pauseBetweenWaves);
            }
        }
    }
}
