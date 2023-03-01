using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public enum Type
    {
        None,
        Slow,
        Fast,
        Long,
        Short,
    };

    public float speed = 8f;
    public float slowMultiplier = 0.7f;
    public float fastMultiplier = 1.5f;
    public float longMultiplier = 1.5f;
    public float shortMultiplier = 0.7f;
    public float duration = 5f;
    public Type type;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
