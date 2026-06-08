using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float speed = 4f;
    public float jumpForce = 20f;
    
    private Vector2 movement;
    private float inputX;
    private bool jumpPressed = false;
    
    public MovingPlatform platform;
  
    
    public SpriteRenderer sr;
    public AudioSource jumpSound;
    public AudioClip jumpClip;


    private float wallJumpForceX = 6f;
    private float wallJumpForceY = 12f;
    
    private float wallJumpLockTime = 0.2f;
    private float wallJumpTimer = 0f;
    
    private bool isTouchingWall = false;
    private int wallDirection = 1;
    
    public Animator anime;
    
    bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        sr = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //左右
        inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            sr.flipX = false;
        }
        else if (inputX < 0)
        {
            sr.flipX = true;
        }
        //アニメーション
        anime.SetFloat("speed",Mathf.Abs(inputX));
        
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            { 
                jumpPressed = true;
            }
            else if (isTouchingWall || isGrounded)
            {
                jumpPressed = true;
            }
        }

        Debug.Log("isGrounded: " + isGrounded);
    }

    void FixedUpdate()
    {   
        if (wallJumpTimer > 0)
        {
            wallJumpTimer -= Time.fixedDeltaTime;
        }
        //左右
        else
        {
            rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);
        }
        
        if (platform != null)
        {
            transform.position += platform.delta; 
        }
        //ジャンプ
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
            else if (isTouchingWall)
            {   
                
                rb.AddForce(
                    new Vector2(-wallDirection * wallJumpForceX,wallJumpForceY),
                    ForceMode2D.Impulse
                    );
                    wallJumpTimer = wallJumpLockTime;
            }

            anime.SetBool("isjump", true);
            jumpSound.PlayOneShot(jumpClip);

            jumpPressed = false;
            isTouchingWall = false;
            isGrounded = false;
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D other) => CheckCollision(other);
    void OnCollisionStay2D(Collision2D other) => CheckCollision(other); // 触れている間も判定を維持
    void CheckCollision(Collision2D other)
    {
        float nx = other.contacts[0].normal.x;
        float ny = other.contacts[0].normal.y;
        if (ny > 0.5f)
        {
                isGrounded = true;
                anime.SetBool("isjump", false);
                //?
            if (other.gameObject.CompareTag("platform"))
            {
                platform = other.gameObject.GetComponent<MovingPlatform>();
            }
        } 
        else if (other.gameObject.CompareTag("wall")&&Mathf.Abs(nx) > 0.7f)
        {
            isTouchingWall = true;
            wallDirection = nx > 0 ? -1 : 1;
        }
        
        

    }

    void OnCollisionExit2D(Collision2D other)
    {   
        
        if (other.gameObject.CompareTag("platform")||other.gameObject.CompareTag("wall"))
        {
            isGrounded = false;
            isTouchingWall = false;
            platform = null;
        }
    }

}
