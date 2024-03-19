using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{

    //**************VARIABLES**************
    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    [Header("Attack details")] 
    //CUSTOM SPEED FOR EACH ATTACK
    public float[] attackMovement;
    public float counterAttackDuration = .2f;
    public bool isBusy { get; private set; }

    public SkillManager skill { get; private set;}
 

    //**************STATES**************

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }




    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "WallJump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        checkForDashInput(); // we put it in player because we need it to be always available
                             //its an excepion based on how we want our game to work




    }

    //**************CORUTINE**************

    public IEnumerator BusyFor(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    //ANIMATION CONTROL
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    
    

    private void checkForDashInput()
    {
        if (IsWallDetected()) { return; }




        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            { //when we dont give input, it will dash in the face direction
                dashDir = facingDir;
            }

            //if (dashDir != 0){ // we dont dash if we have no input, its ignored, only dash when move
            stateMachine.ChangeState(dashState);
            //}
        }

    }


}
