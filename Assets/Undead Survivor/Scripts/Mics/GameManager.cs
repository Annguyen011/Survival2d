using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("# Timer")]
    public float gameTime;
    public float maxGameTimer = 40f;

    [Header("# Data")]
    public int level;
    public int exp;
    public int kill;
    public int[] nextExp = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 100 };

    #region Components
    [Header("# Components")]
    public Player player;
    public PoolManager poolManager;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        FillComponents();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTimer)
        {
            gameTime = maxGameTimer;
        }
    }

    private void FillComponents()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        if (poolManager == null)
        {
            poolManager = GameObject.FindAnyObjectByType<PoolManager>();
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
