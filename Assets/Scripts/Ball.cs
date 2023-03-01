using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject blocksParent;
    public TMP_Text livesText;
    public int lives = 5;
    public float speed = 10f;
    public float speedMultiplier = 1f;
    public float maxVx = 7f;
    public float hitAngleChange = 20f;
    public float spawnMaxX = 3f;
    public float spawnDistance = 4f;
    public float minY = -4.5f;

    private int level = 0;
    private Rigidbody2D rb;
    private int blockLeft;
    private float spawnY;
    private Vector3 spawnHitMark;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject paddle = GameObject.Find("Paddle");
        spawnHitMark = paddle.transform.position;
        spawnY = spawnHitMark.y + spawnDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        updateBlockLeft();
        updateLivesText();
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            updateLivesText();
            if (lives == 0)
            {

            }
            else
            {
                lives--;
                spawn();
            }
        }
    }

    private void spawn()
    {
        float spawnX = Random.Range(-spawnMaxX, spawnMaxX);
        Vector3 spawnVector = spawnHitMark - new Vector3(spawnX, spawnY);
        transform.position = spawnHitMark - spawnVector.normalized * spawnDistance;
        rb.velocity = spawnVector.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            updateBlockLeft();
            if (blockLeft == 0)
            {
                Debug.Log("Cleared");
                SceneManager.LoadScene(++level);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            float hitSpot = transform.position.x - collision.gameObject.transform.position.x;
            float vx = rb.velocity.x + hitSpot * hitAngleChange;
            if (vx > maxVx) vx = maxVx;
            else if (vx < -maxVx) vx = -maxVx;
            rb.velocity = new Vector3(vx, rb.velocity.y).normalized * speed * speedMultiplier;
        }
    }

    public void modifySpeed(float mulitplier)
    {
        speedMultiplier = mulitplier;
    }

    public int resetSpeed()
    {
        speedMultiplier = 1f;
        return 0;
    }

    private void updateBlockLeft()
    {
        blockLeft = blocksParent.transform.childCount;
    }

    private void updateLivesText()
    {
        if (livesText)
        {
            livesText.text = "Lives X " + lives.ToString();
        }
    }
}
