using UnityEditor;
using UnityEngine;

public class ScoreInstance : MonoBehaviour
{
  

    void OnPlayerEnter(PlayerMovement player)
    {

        Score.instance.AddPoint();

    }
}
