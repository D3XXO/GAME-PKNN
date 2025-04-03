using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public GameObject[] objectsToWatch;

    void Update()
    {
        bool allObjectDestroyed = true;

        foreach (GameObject obj in objectsToWatch)
        {
            if (obj != null)
            {
                allObjectDestroyed = false;
                break;
            }
        }

        if (allObjectDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
