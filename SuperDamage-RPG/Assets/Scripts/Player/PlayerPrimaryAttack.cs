using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
        //like riven, if you dont attack for the established combowindow, it resets
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);


        //CHOOSE ATTACK DIRECTION
        float attackDir = player.facingDir;
        if(xInput != 0){
            attackDir = xInput;
        }

        //CUSTOM SPEED FOR EACH ATTACK 
        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, rb.velocity.y);
        /* We can also use comboCounter as array of vectors2 and put custom "hops" in each attack**/


        //COOLDOWN FOR BUSY
        stateTimer = 0.2f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .155f); //time to wait between attacks so it doesnt go
        //for a milisecond in idle mode and therefore moving between attacks

        comboCounter++;
        lastTimeAttacked = Time.time;
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.ZeroVelocity();

        }


        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
