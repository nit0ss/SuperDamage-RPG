using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{

    
    [SerializeField] private float returnSpeed;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private Player player;

    private bool canRotate = true;
    private bool isReturning;

    private int ammountOfBounces;
    [SerializeField] private float bounceSpeed;
    private float bounceGravity;
    
    private bool isBouncing;
    private List<Transform> enemyTargets;
    private int targetIndex;
    //public LayerMask enemyLayer;
    //private float detectionRadius = 10f;



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();

    }
    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotation", true);
    }
    public void SetupBounce(bool _isBouncing,float _bounceGravity, int _ammountOfBounces){
        this.isBouncing = _isBouncing;
        this.ammountOfBounces = _ammountOfBounces;
        this.bounceGravity = _bounceGravity;
    }

    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
        anim.SetBool("Rotation", true);

    }

    //******************MAIN LOGIC******************

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }

        BounceSword();
        
    }


    //******************BOUNCING FEATURE******************


    private void BounceSword(){
        if (isBouncing && enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < .1f)
            {
                targetIndex++;
                ammountOfBounces--;

                if (ammountOfBounces <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTargets.Count)
                    targetIndex = 0;
            }
        }
    }

    //******************ENEMY HIT******************

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTargets.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTargets.Add(hit.transform);
                }
            }
        }
        /* Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, 10f)
            .Where(collider => collider.GetComponent<Enemy>() != null)
            .ToArray(); */


        StuckInto(collision); //INTO ENTEMY OR INTO WALL
    }

    private void StuckInto(Collider2D collision)
    {
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTargets.Count > 0)
            return;

        if (!isReturning)
            anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
