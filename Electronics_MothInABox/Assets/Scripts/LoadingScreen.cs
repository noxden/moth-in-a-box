//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        canvasGroup.alpha = 1;

        StartCoroutine(CheckForBoardConnected());
    }

    IEnumerator CheckForBoardConnected()
    {
        GameManager gameManager = GameManager.Instance;
        while (!gameManager.isBoardConnected)
        {
            yield return new WaitForSeconds(1);
        }

        //Debug.Log(Time.realtimeSinceStartup);
        if (Time.realtimeSinceStartup >= 5.0f)      //< Let the Title Screen linger for a moment, even if the board has already been discovered.
            UiUtil.Instance.FadeOut(canvasGroup, 1f);
    }

    IEnumerator FadeOutCanvas()
    {
        float timeElapsed = 0;
        float lerpDuration = 1;

        while (timeElapsed < lerpDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            Debug.Log($"MOOOOIN", this);

            yield return null;
        }
        canvasGroup.alpha = 0;
    }

}
