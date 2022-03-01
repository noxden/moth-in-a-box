//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 28-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeElement : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    //public bool startVisible = true;
    [Range(0.1f, 2f)]
    public float fadeDuration = 1f;

    private void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        /*
        switch (startVisible)
        {
            case true:
                canvasGroup.alpha = 1;
                break;
            case false:
                canvasGroup.alpha = 0;
                break;
        }
        canvasGroup.interactable = startVisible;
        canvasGroup.blocksRaycasts = startVisible;
        */
    }

    //> Assign any of these in a button to trigger it
    public void FadeIn()    
    {
        //Debug.Log("FadeIn triggered.");
        UiUtil.Instance.Fade(canvasGroup, fadeDuration, true);
    }

    public void FadeOut()
    {
        UiUtil.Instance.Fade(canvasGroup, fadeDuration, false);
    }
}
