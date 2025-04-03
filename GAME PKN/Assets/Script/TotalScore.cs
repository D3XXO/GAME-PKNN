using UnityEngine;

public class TotalScore : MonoBehaviour
{
    public TextMesh textMesh;

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMesh>();
        }

        UpdateScoreDisplay();
    }

    void Update()
    {
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (textMesh != null)
        {
            textMesh.text = "Total Score: " + ScoreManager.instance.score;
        }
    }
}
