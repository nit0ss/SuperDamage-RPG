using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;


    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();

        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {

        rb.velocity = new Vector2(xVelocity, yVelocity);

    }
}
