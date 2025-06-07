using UnityEngine;

public class ShowCanvasOnClick : MonoBehaviour
{
    public GameObject targetCanvas;

    void OnMouseDown()
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
        }
    }
}
