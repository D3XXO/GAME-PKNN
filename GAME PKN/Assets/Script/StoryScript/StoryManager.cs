using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryPart
    {
        [TextArea(3, 10)]
        public string storyText;
        public Sprite backgroundSprite;
        public float typingSpeed = 0.05f;
    }

    public TMP_Text storyTextUI;
    public Image backgroundImage;
    public List<StoryPart> storyParts = new List<StoryPart>();
    private int currentPartIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    [Header("Scene Transition")]
    public string nextSceneName = "Lobby 1";
    public float delayBeforeLoad = 2f;
    public SceneTransition sceneTransition;

    void Start()
    {
        if (storyTextUI == null)
            storyTextUI = GameObject.Find("StoryText").GetComponent<TMP_Text>();
        
        if (backgroundImage == null)
            backgroundImage = GameObject.Find("Background").GetComponent<Image>();

        ShowStoryPart(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                CompleteCurrentText();
            }
            else
            {
                NextPart();
            }
        }
    }

    public void NextPart()
    {
        currentPartIndex++;
        
        if (currentPartIndex < storyParts.Count)
        {
            ShowStoryPart(currentPartIndex);
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        if (sceneTransition != null)
        {
            sceneTransition.StartTransition(nextSceneName);
        }
        else
        {
            yield return new WaitForSeconds(delayBeforeLoad);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ShowStoryPart(int index)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        // Mulai animasi ketik baru
        typingCoroutine = StartCoroutine(TypeText(
            storyParts[index].storyText,
            storyParts[index].typingSpeed
        ));
        
        if (storyParts[index].backgroundSprite != null)
        {
            backgroundImage.sprite = storyParts[index].backgroundSprite;
        }
    }

    private IEnumerator TypeText(string text, float speed)
    {
        isTyping = true;
        storyTextUI.text = "";
        
        foreach (char letter in text.ToCharArray())
        {
            storyTextUI.text += letter;
            
            float currentDelay = speed;
            if (char.IsPunctuation(letter))
            {
                currentDelay *= 3f;
            }
            
            yield return new WaitForSeconds(currentDelay);
        }
        
        isTyping = false;
    }

    private void CompleteCurrentText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        storyTextUI.text = storyParts[currentPartIndex].storyText;
        isTyping = false;
    }
}