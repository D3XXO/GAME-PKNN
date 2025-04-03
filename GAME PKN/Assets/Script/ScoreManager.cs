using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic; // Untuk Dictionary

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public Text scoreText;

    // Dictionary untuk menyimpan poin setiap scene
    private Dictionary<string, int> sceneScores = new Dictionary<string, int>();

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
        }
    }

    void Start()
    {
        FindScoreText();
        LoadScoreForCurrentScene(); // Ambil skor saat scene dimulai
        UpdateScoreUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText();
        LoadScoreForCurrentScene();
    }

    void FindScoreText()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText")?.GetComponent<Text>();
        UpdateScoreUI();
    }

    // Tambah skor
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // Simpan skor saat berpindah scene
    public void SaveScoreForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        sceneScores[currentScene] = score;
    }

    // Ambil skor saat scene berubah
    public void LoadScoreForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (sceneScores.ContainsKey(currentScene))
        {
            score = sceneScores[currentScene]; // Kembalikan skor yang sudah disimpan
        }
        else
        {
            score = GetPreviousScore(); // Ambil skor terakhir jika belum ada di Dictionary
        }

        UpdateScoreUI();
    }

    // Reset semua skor ke 0
    public void ResetScore()
    {
        score = 0;
        sceneScores.Clear(); // Hapus semua skor yang tersimpan
        UpdateScoreUI();
        Debug.Log("Score has been reset to 0");
    }


    // Ambil skor terakhir yang disimpan untuk semua scene
    private int GetPreviousScore()
    {
        int lastScore = 0;

        foreach (var sceneScore in sceneScores.Values)
        {
            lastScore = sceneScore;
        }

        return lastScore;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}