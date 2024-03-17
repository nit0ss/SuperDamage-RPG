using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkelletonAnimationTrigger : MonoBehaviour
{
    private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger(){
        enemy.AnimationFinishTrigger();
    }
}
