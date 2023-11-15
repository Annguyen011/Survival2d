using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;
    private List<Transform> points = new List<Transform>();
    private float timer;
    private int level;
    private PoolingName type;

    private void Start()
    {
        foreach (Transform t in transform)
        {
            points.Add(t);
        }
    }

    private void Update()
    {
        TimeCounter();

    }

    private void TimeCounter()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        switch (level)
        {
            case 0:
                type = PoolingName.Enemy01;
                break;
            case 1:
                type = PoolingName.Enemy02;
                break;
            case 2:
                type = PoolingName.Enemy03;
                break;
            default:
                type = PoolingName.Enemy04;
                break;

        }

        GameObject enemy = GameManager.instance.poolManager.Get(type);
        enemy.transform.position = points[UnityEngine.Random.Range(0, points.Count)].position;
        enemy.GetComponent<Enemy>().Init(data: spawnData[level]);
    }


}
