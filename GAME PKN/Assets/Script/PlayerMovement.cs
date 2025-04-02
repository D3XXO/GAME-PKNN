using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    public CoinManager cm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Cek apakah ada spawn point yang tersimpan
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wrong"))
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0)
        {
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
    }
}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }
}
