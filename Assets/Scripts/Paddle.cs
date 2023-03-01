using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Paddle : MonoBehaviour
{
    public float speed = 5f;
    public float maxX = 2.5f;

    private Vector3 normalTransform;
    private Ball ball;
    private TMP_Text buffText;

    private Buff.Type currentBuff = Buff.Type.None;
    private float buffTimer = 0f;
    private Func<int> removeBuffFunc;

    private void Awake()
    {
        normalTransform = transform.localScale;
        buffText = GameObject.Find("BuffText").GetComponent<TMP_Text>();
        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }

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

        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
            if (buffTimer <= 0)
            {
                removeBuffFunc();
                currentBuff = Buff.Type.None;
                buffText.text = "";
            }
            else
            {
                buffText.text = currentBuff + " " + buffTimer.ToString("F2");
            }
        }
    }

    public void modifyLength(float mulitplier)
    {
        float scaleX = normalTransform.x * mulitplier;
        transform.localScale = new Vector3(scaleX, transform.localScale.y);
    }

    public int resetLength()
    {
        transform.localScale = normalTransform;
        return 0;
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buff"))
        {
            if (currentBuff != Buff.Type.None)
            {
                removeBuffFunc();
            }

            Buff buff = collision.gameObject.GetComponent<Buff>();
            currentBuff = buff.type;
            buffTimer = buff.duration;
            switch (buff.type)
            {
                case Buff.Type.Slow:
                    ball.modifySpeed(buff.slowMultiplier);
                    removeBuffFunc = ball.resetSpeed;
                    break;
                case Buff.Type.Fast:
                    ball.modifySpeed(buff.fastMultiplier);
                    removeBuffFunc = ball.resetSpeed;
                    break;
                case Buff.Type.Long:
                    modifyLength(buff.longMultiplier);
                    removeBuffFunc = resetLength;
                    break;
                case Buff.Type.Short:
                    modifyLength(buff.shortMultiplier);
                    buffTimer = buff.duration;
                    removeBuffFunc = resetLength;
                    break;
            }
            Destroy(collision.gameObject);
        }
        
        
    }
}
