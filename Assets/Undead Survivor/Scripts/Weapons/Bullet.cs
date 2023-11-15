using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("# Bullet info")]
    public float damage;
    public int per;

    #region Components
    private Rigidbody2D rb;

    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    public void Init(float damage, int per, Vector2 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per != -1)
        {
            rb.velocity = dir * 15f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
        {
            return;
        }

        per--;

        if (per == -1)
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
