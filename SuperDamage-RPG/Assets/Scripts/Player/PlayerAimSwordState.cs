using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        originalFacingDirection = player.facingRight;
        player.skill.sword.DotsActive(true);
    }
    public override void Exit()
    {
        base.Exit();
        if (player.facingRight != originalFacingDirection)
        {
            player.Flip(); // Asume que Flip cambia la orientación del personaje
        }
        player.skill.sword.DotsActive(false); // Desactiva la visualización de la trayectoria de la espada
    }


    public override void Update()
    {
        player.SetZeroVelocity();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float direction = player.transform.position.x - mousePosition.x;
        // If direction is positive, the cursor is to the right of the character; if negative, it's to the left.
        if ((direction < 0 && !player.facingRight) || (direction > 0 && player.facingRight))
        {
            player.Flip(); // Assumes you have a Flip function that changes the character's orientation
        }

        base.Update();
        if (Input.GetKeyUp(KeyCode.E))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}


/*         Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x < mousePosition.x) //aiming to the left
        {
            player.anim.transform.Rotate(0, 180, 0);
        }
 */