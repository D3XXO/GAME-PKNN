using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.SaveScoreForCurrentScene(); // Simpan poin dulu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Kembali ke scene sebelumnya
        }
    }
}