using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THIS CLASS IS MADE FOR THE ANIMATOR, THE REAL METHOD IS IN PLAYER.CS ANIMATIONTRIGGER()
public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
}
