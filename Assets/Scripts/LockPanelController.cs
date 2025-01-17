using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockPanelController : MonoBehaviour
{
    public GameObject LockPanel; // UI panel for the lock
    public TextMeshProUGUI LetterText; // Text with a random letter
    private char currentLetter;

    private void Awake()
    {
        if (LockPanel != null)
        {
            DontDestroyOnLoad(LockPanel);
        }
    }

    private void OnEnable()
    {
        Events.OnSetLockBarLetter += ActivateLock;
        Events.OnUnlockPanel += DeactivateLock;
    }

    private void OnDisable()
    {
        Events.OnSetLockBarLetter -= ActivateLock;
        Events.OnUnlockPanel -= DeactivateLock;
    }

    private void ActivateLock(char letter)
    {
        
        currentLetter = letter;

        if (LetterText == null)
        {
            return;
        }

        LetterText.text = currentLetter.ToString();
        Debug.Log("Current letter: " + currentLetter);
        LockPanel.SetActive(true);
        FindObjectOfType<InGameGuide>().OnLockPanelAppear();
    }

    private void DeactivateLock()
    {
        if (LockPanel == null)
        {
            return;
        } 
        LockPanel.SetActive(false);
        FindObjectOfType<InGameGuide>().OnLockPanelComplete();
    }

    private void Update()
    {
        if (LockPanel != null && LockPanel.activeSelf && Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                Debug.Log("Pressed key: " + c); // which letter pressed check
                if (c == currentLetter)
                {
                    Debug.Log("Correct letter pressed!");
                    Events.UnlockPanel();
                    break;
                }
            }
        }
    }
}
