using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5f;
    public float maxX = 2.9f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        Vector3 newPos = transform.position + Vector3.right * movement * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(newPos.x, -maxX, maxX), newPos.y, newPos.z);
    }
}
