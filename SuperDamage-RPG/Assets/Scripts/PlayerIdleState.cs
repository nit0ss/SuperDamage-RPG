using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }


    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(xInput == player.facingDir && player.IsWallDetected())
            return;

        if(xInput != 0) 
            stateMachine.ChangeState(player.moveState);


    }
}
