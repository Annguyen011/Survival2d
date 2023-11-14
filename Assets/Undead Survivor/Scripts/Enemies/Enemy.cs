using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header("Movement info")]
    [SerializeField] private float speed;

    [Header("Combat info")]
    [SerializeField] private Rigidbody2D target;
    private bool isLive = true;

    #region Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (target == null)
        {
            target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.mass = 5f;
    }

    private void FixedUpdate()
    {
        if(!isLive)
        {
            return;
        }

        Movement();
    }

    private void LateUpdate()
    {
        Flip();
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

}
