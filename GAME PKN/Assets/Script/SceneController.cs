using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)] // Eksekusi lebih awal
public class SceneController : MonoBehaviour
{
    private static bool hasStartedFromFirstScene = false;

    private void Awake()
    {
        // Jika baru pertama kali play, mulai dari scene index 0
        if (!hasStartedFromFirstScene)
        {
            hasStartedFromFirstScene = true; // Tandai bahwa sudah dimulai dari awal
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0); // Pindah ke scene index 0
            }
        }
    }
}