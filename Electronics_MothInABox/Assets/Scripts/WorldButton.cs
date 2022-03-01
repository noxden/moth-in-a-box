//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 01-03-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider), typeof(SceneChangeUtil))]
public class WorldButton : MonoBehaviour
{
    private static float activationTime = 4;

    private IEnumerator coroutine;
    private float timeRemaining;
    private Canvas canvas;
    private Text counterText;
    private SceneChangeUtil sceneChangeScript;

    public bool isContinueButton = true;

    private void Start()
    {
        
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = FindObjectOfType<Camera>();
        counterText = canvas.GetComponentInChildren<Text>();
        sceneChangeScript = GetComponent<SceneChangeUtil>();

        coroutine = CountDown();

        counterText.text = string.Empty;
}

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(coroutine);
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (timeRemaining <= activationTime)
        timeRemaining += Time.deltaTime;
        else if (!isActivated)
        {
            isActivated = true;
            sceneChangeScript.ChangeScene();
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(coroutine);
        counterText.text = "";
    }

    IEnumerator CountDown()
    {
        timeRemaining = activationTime;
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            counterText.text = timeRemaining.ToString();
            timeRemaining -= 1;
        }
        if (isContinueButton)
            sceneChangeScript.ChangeScene();
        else
            Application.Quit();
    }
}
