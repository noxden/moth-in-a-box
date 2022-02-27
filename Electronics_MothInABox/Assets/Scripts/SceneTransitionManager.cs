//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Jan Alexander (AR Art- & App-Development), Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene       // These enums can be called by Scene.NAME
{
    CalibrationMenu, MainMenu, Tutorial, SandboxScene
}

public sealed class SceneTransitionManager
{
    // Public Methods
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadSceneAsync(SceneName(scene));
    }

    // Helper Methods
    private static string SceneName(Scene scene)
    {
        switch (scene)
        {
            case Scene.CalibrationMenu:
                return "CalibrationMenu";
            case Scene.MainMenu:
                return "MainMenu";
            case Scene.Tutorial:
                return "Tutorial";
            case Scene.SandboxScene:
                return "SandboxScene";
        }
        return "";
    }
}