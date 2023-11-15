using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(PoolingName index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[(int)index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[(int)index],transform);
            pools[(int)index].Add(select);
        }

        return select;
    }
}

public enum PoolingName
{
    Enemy01,
    Enemy02,
    Enemy03,
    Enemy04,
    Enemy05,
    Bullet0,
    Bullet1
}
