using UnityEngine;

public class Stomp : MonoBehaviour
{
    public Rigidbody2D rb;
    
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        audioSource = GetComponentInParent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {   
            rb.linearVelocity = Vector2.zero;
            Destroy(other.gameObject);
        }
    }
}
