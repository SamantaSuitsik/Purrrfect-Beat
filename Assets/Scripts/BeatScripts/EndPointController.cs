using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor; 
    public Color UltraHitColor = Color.green;
    public Color HitColor = Color.yellow;
    public Color BadHitColor = new Color(1f, 0.647f, 0f);
    public Color missColor = Color.red;
    int currentLevel = GameManager.Instance.CurrentLevel;

    public float colorChangeDuration = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    
    public void OnUltraHit()
    {
        StartCoroutine(ChangeColor(UltraHitColor));

        if (currentLevel == 1)
        {
            FindObjectOfType<InGameGuide>().OnBeatHit();
        }
        
    }
    public void OnHit()
    {
        StartCoroutine(ChangeColor(HitColor));
        if (currentLevel == 1)
        {
            FindObjectOfType<InGameGuide>().OnBeatHit();
        }
       
    }
    public void OnBadHit()
    {
        StartCoroutine(ChangeColor(BadHitColor));
        if (currentLevel == 1)
        {
            FindObjectOfType<InGameGuide>().OnBeatHit();
        }
        
    }


    public void OnMiss()
    {
        StartCoroutine(ChangeColor(missColor));
    }

    
    private IEnumerator ChangeColor(Color newColor)
    {
        spriteRenderer.color = newColor;

        yield return new WaitForSeconds(colorChangeDuration);

        spriteRenderer.color = originalColor;
    }
}
