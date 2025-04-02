using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static Score instance;

    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = score.ToString() + "POINTS";
    }

    // Update is called once per frame
    public void AddPoint()
    {
        score += 30;
        scoreText.text = score.ToString() + "POINTS";
    }
}
