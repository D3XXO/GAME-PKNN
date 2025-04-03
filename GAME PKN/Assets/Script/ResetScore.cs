using UnityEngine;

public class ResetScore : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.ResetScore();
        }
    }
}