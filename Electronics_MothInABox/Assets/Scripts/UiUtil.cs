//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 27-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUtil : MonoBehaviour
{
    static public UiUtil Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region Fade
    public void Fade(CanvasGroup _canvasGroup, float _lerpDuration, bool _doFadeIn)
    {
        if (_canvasGroup == null)
            return;
        StartCoroutine(crFade(_canvasGroup, _lerpDuration, _doFadeIn));
    }
    private IEnumerator crFade(CanvasGroup _canvasGroup, float _lerpDuration, bool _doFadeIn = false)
    {
        float startValue;
        float endValue;

        switch (_doFadeIn)
        {
            case true:
                startValue = 0;
                endValue = 1;
                break;
            case false:
                startValue = 1;
                endValue = 0;
                break;
        }

        if (_doFadeIn == false)  //< If fade out, then run this code here.
        {
            _canvasGroup.interactable = _doFadeIn;
            
        }
        else                     //< If fade in, then run this code here.
        {
            _canvasGroup.blocksRaycasts = _doFadeIn;
        }
            

        float timeElapsed = 0;

        while (timeElapsed <= _lerpDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(startValue, endValue, timeElapsed / _lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        switch (_doFadeIn)
        {
            case true:
                _canvasGroup.alpha = 1;
                break;
            case false:
                _canvasGroup.alpha = 0;
                break;
        }

        if (_doFadeIn == true)  //< If fade in, then run this code here.
        {
            _canvasGroup.interactable = _doFadeIn;

        }
        else                     //< If fade out, then run this code here.
        {
            _canvasGroup.blocksRaycasts = _doFadeIn;
        }
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

    #region Toggle
    public void Toggle(CanvasGroup _canvasGroup, bool _toggleOn)
    {
        if (_toggleOn)
            _canvasGroup.alpha = 1;
        else
            _canvasGroup.alpha = 0;

        _canvasGroup.interactable = _toggleOn;
        _canvasGroup.blocksRaycasts = _toggleOn;
    }
#endregion
}
