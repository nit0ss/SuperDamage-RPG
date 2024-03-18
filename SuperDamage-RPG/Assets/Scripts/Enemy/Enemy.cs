using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Entity
{
   [SerializeField] protected LayerMask whatIsPlayer;

   [Header("Stunned Info")]
   public float stunDuration;
   public Vector2 stunDirection;
   protected bool canBeStunned;
   [SerializeField] protected GameObject counterImage;

   [Header("Move Info")]
   public float moveSpeed;
   public float idleTime;
   [Header("Attack Info")]
   [SerializeField] public float attackDistance;
   [SerializeField] public float attackCooldown;
   [SerializeField] public float detectDistance;
   [HideInInspector] public float lastTimeAttacked;
   //[HideInInspector] public float flipCooldown = 0.25f;
   public float battleTime;

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

   protected override void OnDrawGizmos()
   {
      base.OnDrawGizmos();
      Gizmos.color = Color.yellow;
      Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
   }


   public virtual void OpenCounterAttackWindow()
   {
      canBeStunned = true;
      counterImage.SetActive(true);
   }
   public virtual void CloseCounterAttackWindow()
   {
      canBeStunned = false;
      counterImage.SetActive(false);
   } 

   public virtual bool CanBeStunned(){
      if(canBeStunned){
         CloseCounterAttackWindow();
         return true;
      }
      else
      {
         return false;
      }
   }



   //LAMBDA CONTROLS
   public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, detectDistance, whatIsPlayer);
   public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();



   /*public virtual RaycastHit2D IsPlayerDetected(){
      RaycastHit2D playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, detectDistance, whatIsPlayer);
      
      //WALL FIX PATCH/
      RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, detectDistance + 1, whatIsGround);
      if(wallDetected){
         return default(RaycastHit2D);
         }
      return playerDetected;   
   } */

   

}
