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

    [Header("Audio")]
    public AudioClip enemyHitSound;
    private AudioSource audioSource;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();

        string spawnPointName = PlayerPrefs.GetString("SpawnPoint", "");
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawnPoint = GameObject.Find(spawnPointName);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
        }

        audioSource = GetComponent<AudioSource>();
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
            if (enemyHitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(enemyHitSound);
            }
            
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delayBeforeShow);
        gameOverCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
