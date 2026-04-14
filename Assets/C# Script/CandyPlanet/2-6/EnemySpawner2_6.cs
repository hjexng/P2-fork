using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner2_6 : MonoBehaviour
{
    [Header("Prefab to Spawn")]
    public GameObject prefab; // 생성할 프리팹

    [Header("Spawn Range (Local X)")]
    public float leftX = -300f;
    public float rightX = 300f;

    public float spawnY = 0f;

    public MiniGame2_6 minigame;

    public void SpawnObstacle()
    {
        // 3등분 중 하나 선택
        int lane = Random.Range(0, 3);

        float width = (rightX - leftX) / 3f;
        float startX = leftX + lane * width;

        // 2/3 크기 장애물
        float obstacleWidth = width * (2f / 3f);

        float spawnX = startX + (width / 2f);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

        Enemy2_6 enemy = obj.GetComponent<Enemy2_6>();
        enemy.Init(minigame);

        // 크기 조절
        obj.transform.localScale = new Vector3(obstacleWidth, obj.transform.localScale.y, 1f);
    }
}
