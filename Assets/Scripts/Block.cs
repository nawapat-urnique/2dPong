using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private const int maxHp = 5;

    public int hp = 1;
    public Gradient gradient;
    public Buff.Type buff = Buff.Type.None;
    public GameObject buffObject;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        updateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (--hp <= 0)
        {
            Destroy(gameObject);
            if (buff != Buff.Type.None)
            {
                GameObject buffObj = Instantiate(buffObject, transform.position, Quaternion.identity);
                Buff buff = buffObj.GetComponent<Buff>();
                buff.type = this.buff;
            }
        }
        else
        {
            updateColor();
        }
    }

    private void updateColor()
    {
        sr.color = gradient.Evaluate((float) (hp - 1) / maxHp);
    }
}
