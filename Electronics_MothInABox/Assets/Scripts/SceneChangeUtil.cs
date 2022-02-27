//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144), Jan Alexander (AR Art- & App-Development)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SceneChangeUtil : MonoBehaviour
{
    public Scene sceneToLoad;

    // Private Helpers
    public void ChangeScene()
    {
        SceneTransitionManager.LoadScene(sceneToLoad);
    }
}

