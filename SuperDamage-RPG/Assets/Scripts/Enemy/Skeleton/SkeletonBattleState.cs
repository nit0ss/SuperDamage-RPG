using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{

    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;
    private bool samePositionAsPlayer => player.position.x == enemy.transform.position.x;
    // Variables para el cooldown
    private float lastDirectionCheckTime = 0f;
    private readonly float directionCheckCooldown = 0.35f; // Cooldown de 0.25 segundos


    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform; //we change it later because this takes too much resources



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
            if ((stateTimer < 0) || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.detectDistance)
            {
                stateMachine.ChangeState(enemy.idleState);

            }
        }

        

// Verifica si ha pasado el cooldown antes de llamar a CheckAttackDirection
        if (Time.time - lastDirectionCheckTime >= directionCheckCooldown)
        {
            CheckAttackDirection();
            lastDirectionCheckTime = Time.time; // Actualiza el tiempo de la última comprobación
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

    private void CheckAttackDirection()
    {

        if (player.position.x > enemy.transform.position.x && !samePositionAsPlayer)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x && !samePositionAsPlayer)
        {
            moveDir = -1;
        }
    }


}
