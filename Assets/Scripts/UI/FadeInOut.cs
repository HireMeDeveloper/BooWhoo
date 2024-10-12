using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    private bool fadeIn = false;
    private bool fadeOut = false;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();    
    }

    void Update()
    {

        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                    fadeIn = false;
            }
        }

        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if (canvasGroup.alpha == 0)
                    fadeOut = false;
            }
        }
    }

    public void FadeIn()
    {
        fadeOut = false;
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeIn = false;
        fadeOut = true;
    }

}
