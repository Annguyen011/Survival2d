using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header("# Movement info")]
    [SerializeField] private float speed;

    [Header("# Combat info")]
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float health;
    [SerializeField] private Rigidbody2D target;
    private WaitForFixedUpdate waitForUpdate;
    private bool isLive;

    #region Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Collider2D coll;
    #endregion

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        waitForUpdate = new WaitForFixedUpdate();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        health = maxHealth;

        if (target == null)
        {
            target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        }

        isLive = true;
        coll.enabled = true;
        rb.simulated = true;
        sprite.sortingOrder = 10;

        animator.SetBool("Dead", false);
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.mass = 5f;
    }

    private void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Movement();
    }

    private void LateUpdate()
    {
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }
        StartCoroutine(Knockback());
        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;
            sprite.sortingOrder = 1;
            animator.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            Invoke(nameof(Dead), .7f);
        }
    }

    private IEnumerator Knockback()
    {
        yield return waitForUpdate;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);

    }

    public void Init(SpawnData data)
    {
        maxHealth = data.health;
        health = data.health;
        speed = data.speed;

    }

    private void Movement()
    {
        Vector2 dirVec = target.position - rb.position;
        dirVec.Normalize();

        rb.velocity = Vector2.zero;
        rb.MovePosition(rb.position + dirVec * speed * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        sprite.flipX = rb.position.x > target.position.x;
    }



    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
