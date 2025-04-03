using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            ScoreManager.instance.AddScore(1); // Tambah skor
            Destroy(other.gameObject); // Hapus koin
        }

        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject); // Hapus key
        }
    }
}
