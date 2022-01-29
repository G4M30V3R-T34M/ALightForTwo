using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadAlienSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] BadAlienSpawnerScriptable spawnerData;

    ObjectPool badAlienPool;
    float timeFromLastSpawn;

    void Awake() {
        badAlienPool = GetComponent<ObjectPool>();
    }

    private void Start() {
        timeFromLastSpawn = - spawnerData.StartingWaitTime;
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        while (true) {
            yield return null;
            timeFromLastSpawn += Time.deltaTime;
            if (timeFromLastSpawn > NextSpawnTime()) {
                SpawnEnemy();
                timeFromLastSpawn = 0f;
            }
        }
    }

    private float NextSpawnTime() {
        int lights = BadAlienMind.Instance.getAllLights().Count;
        return (spawnerData.n / (lights + 1)) + (spawnerData.n / (lights + spawnerData.m));
    }

    private void SpawnEnemy() {
        Transform spawnPoint = ChooseRandomSpawnPoint();
        BadAlienController badAlien = (BadAlienController)badAlienPool.getNext();
        badAlien.transform.position = spawnPoint.transform.position;
        badAlien.gameObject.SetActive(true);
    }

    private Transform ChooseRandomSpawnPoint() {
        int idx = Random.Range(0, spawnPoints.Length);
        return spawnPoints[idx];
    }
}
