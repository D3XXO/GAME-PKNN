using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    private AudioSource audioSource;
    [SerializeField] private string stopAtScene = "Start";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.activeSceneChanged += OnSceneChanged;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource.Play();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        if (next.name == stopAtScene)
        {
            Destroy(gameObject); // Audio langsung berhenti saat scene "Start" dimuat
        }
    }

    public void LoadMenuScene()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        SceneManager.LoadScene("Start");
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
