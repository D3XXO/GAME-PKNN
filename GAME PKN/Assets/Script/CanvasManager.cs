using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    private List<string> allowedScenes = new List<string> { "Lobby 1", "Main 1", "Lobby 2", "Main 2", "Lobby 3", "Main 3" }; // Scene yang BOLEH menampilkan UI

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(allowedScenes.Contains(scene.name)); // Aktifkan hanya di scene tertentu
    }
}
