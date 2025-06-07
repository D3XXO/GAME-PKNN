using UnityEngine;
using UnityEngine.EventSystems;

public class HideCanvasOnClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
    }
}
