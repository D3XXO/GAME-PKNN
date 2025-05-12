using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject gameOverCanvas;
    public float delayBeforeShow = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();

        // Check if there's a saved spawn point
        string spawnPointName = PlayerPrefs.GetString("SpawnPoint", "");
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
        }
    }

    void Update()
    {
        // Get input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Handle animation states
        if (moveDirection != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            // Update current movement animation
            animator.SetFloat("InputX", moveDirection.x);
            animator.SetFloat("InputY", moveDirection.y);

            // Store last direction for idle state
            animator.SetFloat("LastInputX", moveDirection.x);
            animator.SetFloat("LastInputY", moveDirection.y);
        }
        else
        {
            // No movement, set to idle animation
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wrong"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex > 0)
            {
                SceneManager.LoadScene(currentSceneIndex - 1);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(delayBeforeShow);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
