using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void OnEnable()
    {
        Invoke(nameof(Despawn), lifeTime);
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}
