using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{

    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform; //we change it later
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        /*
        if(!enemy.IsGroundDetected()){
            stateMachine.ChangeState(enemy.idleState);
        }*/

        
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.detectDistance)
            {
                stateMachine.ChangeState(enemy.idleState);

            }
        }


        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        //SO IT DOESNT CHASE US TO DEATH if hes going to fall
        if (!enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();

        }
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            return true;
        }

        else return false;
    }


}
