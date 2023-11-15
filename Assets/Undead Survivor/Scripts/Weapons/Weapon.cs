using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("# Weapon info")]
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    [Header("# Timer info")]
    [SerializeField] private float timer;

    #region Components
    private Player player;
    #endregion

    private void Start()
    {
        player = GameManager.instance.player;
        Init();
    }


    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    private void Fire()
    {
        if (!player.scaner.nearsetTarget)
        {
            return;
        }

        Vector3 targetPos = player.scaner.nearsetTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir.Normalize();

        Transform bullet = GameManager.instance.poolManager.Get(PoolingName.Bullet1).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            default:
                speed = .3f;
                break;
        }
    }

    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(PoolingName.Bullet0).transform;
            }
            bullet.transform.parent = transform;

            bullet.localRotation = Quaternion.identity;
            bullet.localPosition = Vector3.zero;


            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }
}
