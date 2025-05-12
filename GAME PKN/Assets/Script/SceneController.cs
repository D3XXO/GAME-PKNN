using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class SceneController : MonoBehaviour
{
    private static bool hasStartedFromFirstScene = false;

    private void Awake()
    {
        if (!hasStartedFromFirstScene)
        {
            hasStartedFromFirstScene = true;
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}