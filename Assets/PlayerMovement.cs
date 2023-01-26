using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public float levelDuration = 60f;
    public TMP_Text timerText;
    public float gravityModifier = 2f;

    private Rigidbody2D rb;
    private float remainingTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingTime = levelDuration;
        rb.gravityScale *= gravityModifier;
    }

    void Update()
    {
        if (health <= 0)
        {
            EndGame();
            return;
        }

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            EndGame();
            return;
        }

        timerText.text = "Time Remaining: " + Mathf.Round(remainingTime);

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveX * speed, moveY * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 10;
            Debug.Log("Player health: " + health);
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}