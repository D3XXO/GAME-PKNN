using UnityEngine;
using UnityEngine.SceneManagement; // Untuk mengganti scene

public class ChangeSceneOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Cek klik kiri mouse
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                SceneManager.LoadScene("Lobby 1");
            }
        }
    }
}