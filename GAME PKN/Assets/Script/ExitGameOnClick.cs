using UnityEngine;

public class ExitGameOnClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}