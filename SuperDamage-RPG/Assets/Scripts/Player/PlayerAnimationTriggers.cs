using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//THIS CLASS IS MADE FOR THE ANIMATOR, THE REAL METHOD IS IN PLAYER.CS ANIMATIONTRIGGER()
public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
