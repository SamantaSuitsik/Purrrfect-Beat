using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor; 
    public Color hitColor = Color.green;
    public Color missColor = Color.red;

    public float colorChangeDuration = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    
    public void OnHit()
    {
        StartCoroutine(ChangeColor(hitColor));
    }

    
    public void OnMiss()
    {
        StartCoroutine(ChangeColor(missColor));
    }

    
    private System.Collections.IEnumerator ChangeColor(Color newColor)
    {


        spriteRenderer.color = newColor;

        yield return new WaitForSeconds(colorChangeDuration);

        spriteRenderer.color = originalColor;
    }
}
