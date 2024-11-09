using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour
{
    void Start()
    {
        // Get all Button components in children
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            // Add EventTrigger component to the button if not present
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = button.gameObject.AddComponent<EventTrigger>();
            }

            // Set up the hover (pointer enter) event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { PlayHoverSound(button); });
            trigger.triggers.Add(entry);
        }
    }

    void PlayHoverSound(Button button)
    {
        // Retrieve the AudioSource and AudioSound components attached to the button
        AudioSource audioSource = button.GetComponent<AudioSource>();
   

        // Play the button's unique hover sound if available
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
