using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float timeSpawn = 2f;

    private List<Transform> points = new List<Transform>();
    private float timer;
    private int enemyRan;
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

        if (timer > timeSpawn)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        enemyRan = UnityEngine.Random.Range(0, (int)Enum.GetNames(typeof(PoolingName)).Length);

        switch (enemyRan)
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
            case 3:
                type = PoolingName.Enemy04;
                break;

        }

        GameObject enemy = GameManager.instance.poolManager.Get(type);
        enemy.transform.position = points[UnityEngine.Random.Range(0, points.Count)].position;
    }
}
