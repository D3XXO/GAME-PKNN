using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public GameObject door; // Fixed typo from 'dooor' to 'door'
    private bool doorDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        doorDestroyed = false; // Initialize doorDestroyed to false
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString();

        // Check if coinCount is 2 and door has not been destroyed yet
        if (coinCount == 6 && !doorDestroyed)
        {
            doorDestroyed = true; // Set to true to prevent multiple destructions
            Destroy(door); // Destroy the door
        }
    }

    // Example method to collect coins
    public void CollectCoin()
    {
        coinCount++;
    }
}
