using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    [Header("動きの設定")]
    public float amplitude = 0.2f; // 上下の振れ幅（どれくらい動くか）
    public float speed = 5.0f;     // 動く速さ（どれくらい細かく動くか）

    private Vector3 startPos;

    void Start()
    {
        // 最初の位置を記録
        startPos = transform.localPosition;
    }

    void Update()
    {
        // サイン波を使って上下のオフセットを計算
        float newY = Mathf.Sin(Time.time * speed) * amplitude;
        
        // 新しい位置を適用
        transform.localPosition = startPos + new Vector3(0, newY, 0);
    }
}