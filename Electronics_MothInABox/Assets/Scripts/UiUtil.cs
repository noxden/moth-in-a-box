//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUtil : MonoBehaviour
{
    static public UiUtil Instance { set; get; }

    private void Awake()    //Awake() is run even before Start()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one
        }
    }

    #region FadeOut
    public void FadeOut(CanvasGroup _canvasGroup, float _lerpDuration)
    {
        if (_canvasGroup == null)
            return;
        StartCoroutine(crFadeOut(_canvasGroup, _lerpDuration));
    }
    private IEnumerator crFadeOut(CanvasGroup _canvasGroup, float _lerpDuration)
    {
        float timeElapsed = 0;

        while (timeElapsed < _lerpDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        _canvasGroup.alpha = 0;
    }
    #endregion


    #region Blink 
    /*
    public void Blink(CanvasGroup _canvasGroup, float _frequency, float _maximumAlpha)
    {
        StartCoroutine(crBlink(_canvasGroup, _frequency, _maximumAlpha));
    }
    private IEnumerator crBlink(CanvasGroup _canvasGroup, float _frequency, float _maximumAlpha)
    {
        _canvasGroup.alpha = Mathf.Sin(1 + Time.realtimeSinceStartup * _frequency) * _maximumAlpha;
        StartCoroutine(crBlink(_canvasGroup, _frequency, _maximumAlpha));
        return null;
    }
    */
    #endregion

}
