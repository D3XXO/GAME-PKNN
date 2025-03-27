using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk mengelola scene

public class SceneChanger : MonoBehaviour
{
    // Nama scene tujuan
    [SerializeField] private string targetScene;

    // Deteksi saat player menyentuh objek ini
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Pastikan player memiliki tag "Player"
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}