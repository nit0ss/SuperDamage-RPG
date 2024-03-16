using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{


    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    //**************COMPONENTS**************

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    //**************MOVE**************
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);


    //**************FORFLIP**************
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;



    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();

    }
    protected virtual void Update()
    {

    }

    //**************LAMBDA CONTROLL**************

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    //**************FLIP CONTROL**************

    public void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }


    //**************DEBUG**************
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

}
