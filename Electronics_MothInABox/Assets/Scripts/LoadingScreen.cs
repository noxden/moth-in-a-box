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

    public Text connectingText;

    private void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        StartCoroutine(CheckForBoardConnected());
    }

    IEnumerator CheckForBoardConnected()
    {
        GameManager gameManager = GameManager.Instance;

        int timePassed = 0;
        while (!gameManager.isBoardConnected)
        {
            if (timePassed == 29)   //< Text changes once after 30 seconds
            {
                connectingText.text = "<color=#FF7F7F>Failed to establish connection. Please restart the application.</color>";
                timePassed += 1;
            }
            else if (timePassed < 29)
            {
                timePassed += 1;
            }
            yield return new WaitForSeconds(1);
        }

        //Debug.Log(Time.realtimeSinceStartup);
        if (Time.realtimeSinceStartup >= 5.0f)      //< Let the Title Screen linger for a moment, even if the board has already been discovered.
            UiUtil.Instance.Fade(canvasGroup, 1f, false);
    }
}
