using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }

        if (player.IsGroundDetected())//rb.velocity.y == 0)
            stateMachine.ChangeState(player.idleState);

        if (player.IsWallDetected() && rb.velocity.y < 0) //hacer wallslide solo cuando caigas
        {
            stateMachine.ChangeState(player.wallSlide);
        }

    }


}
