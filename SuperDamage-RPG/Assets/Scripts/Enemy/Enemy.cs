using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
   public Rigidbody2D rb { get; private set; }
   public Animator anim { get; private set; }

   public EnemyStateMachine stateMachine { get; private set; }

   protected override void Awake()
   {
      base.Awake();
      stateMachine = new EnemyStateMachine();
   }

   protected override void Update()
   {
      base.Update();
      stateMachine.currentState.Update();
      

   }

   //protected override void 

}
