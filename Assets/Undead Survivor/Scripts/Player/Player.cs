using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("# Movement info")]
    public Vector2 inputVec;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timeSmooth;
    private Vector2 vecSmooth = Vector2.zero;

    #region Components
    public Scaner scaner;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    #endregion

    private void Awake()
    {
        scaner = GetComponent<Scaner>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.mass = 50f;
    }

    private void Update()
    {
        Movement();
        AnimationController();
    }
    private void LateUpdate()
    {
        Flip();
    }

    private void AnimationController()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
    }

    private void Movement()
    {
        Vector2 smoothPosition = Vector2.SmoothDamp(transform.position, inputVec, ref vecSmooth, timeSmooth * Time.deltaTime);

        rb.velocity = speed * smoothPosition;
    }


    private void Flip()
    {
        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
