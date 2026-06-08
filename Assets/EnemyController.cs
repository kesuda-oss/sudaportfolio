using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 4f;

    private int directon = 1;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      rb.linearVelocity = new Vector2(directon * speed, rb.linearVelocity.y);  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        directon *= -1;
        if (directon > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    } 
}
