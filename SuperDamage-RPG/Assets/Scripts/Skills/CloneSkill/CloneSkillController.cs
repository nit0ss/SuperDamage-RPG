using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //for sorting and .where
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{

    [SerializeField] private float colorLosingSpeed;
    private SpriteRenderer sr;
    private Animator anim;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    //private Transform closestEnemy;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
        }
        if (sr.color.a <= 0)
        {
            Destroy(gameObject);
        }


    }

    public void SetUpClone(Transform _newTransform, float _cloneDuration, bool _canAttack)
    {
        if (_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        transform.position = _newTransform.position;
        FaceClosestTarget();


        cloneTimer = _cloneDuration;
    }

    /* 
        //make him face enemy
        private void FaceClosestTarget()
        {
            //we collect the colliders of the objets in this cirlce area
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 40);
            float closestDistance = Mathf.Infinity;

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hit.transform;
                    }

                }


            }

            if (closestEnemy != null)
            {
                if (transform.position.x > closestEnemy.transform.position.x)
                {
                    transform.Rotate(0, 180, 0);
                }
            }


        } */


    private void FaceClosestTarget()
    {
        var closestEnemy = Physics2D.OverlapCircleAll(transform.position, 25)
    .Where(hit => hit.GetComponent<Enemy>() is not null)
    .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
    .FirstOrDefault();

        if (closestEnemy != null)
        {
            //Debug.Log("Closest enemy detected : " + closestEnemy.gameObject.name);

            if (transform.position.x > closestEnemy.transform.position.x)
            {
                transform.Rotate(0, -180, 0);
            }
        }
    }

    /*  private void FaceClosestTarget()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackCheckRadius*2);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    int yRotation = (int)transform.rotation.y;
                    if ((yRotation == 0 && transform.position.x > enemy.transform.position.x) ||
                        (yRotation != 0 && transform.position.x < enemy.transform.position.x))
                        transform.Rotate(0, 180, 0);
                    break;
                }
            }
        }
     */



    //ANIMATION TRIGGER

    private void AnimationTrigger()
    {
        cloneTimer = -1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
