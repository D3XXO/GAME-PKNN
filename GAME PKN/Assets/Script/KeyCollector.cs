using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    public AudioClip keyCollectSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);

            if (keyCollectSound != null)
            {
                audioSource.PlayOneShot(keyCollectSound);
            }
        }
    }
}
