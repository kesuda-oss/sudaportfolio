using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 4f;
    public float distance = 4f;

    public enum Direction
    {
        Horizontal,
        Vertical,
        Both
    }
    public Direction direction = Direction.Horizontal;
    
    Vector2 startPos;
    Vector2 endPos;
    Vector2 targetPos;

    public Vector3 delta;
    private Vector3 lastPos;
    
    void Start()
    {
       startPos = transform.position;
       switch (direction)
       {
           case Direction.Horizontal:
               endPos = startPos + new Vector2(distance,0f);
               break;
           case Direction.Vertical:
               endPos = startPos + new Vector2(0f, distance);
               break;
           case Direction.Both:
               endPos = startPos + new Vector2(distance, distance);
               break;
       }
       targetPos = endPos;
       lastPos = transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position , targetPos, speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = (targetPos == startPos ? endPos : startPos);
        }
        delta = transform.position - lastPos;
        lastPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
