using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    #region Components
    [Header("Components")]
    public Player player;
    public PoolManager poolManager;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        FillComponents();
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
}
