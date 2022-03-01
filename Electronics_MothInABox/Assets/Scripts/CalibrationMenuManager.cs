//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 28-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationMenuManager : MonoBehaviour
{
    private enum Screen
    {
        Main, CalibrationMax, CalibrationMin
    }

    public static CalibrationMenuManager Instance { set; get; }

    private GameManager gameManager;
    private Screen currentScreen;
    private bool isCalibratedMax = false;
    private bool isCalibratedMin = false;

    public bool DebugDeletePlayerPrefs = false;

    public Text maxNorth;   //> Not good, but it works...
    public Text maxEast;
    public Text maxSouth;
    public Text maxWest;

    public Text minNorth;
    public Text minEast;
    public Text minSouth;
    public Text minWest;

    public CanvasGroup loadingScreen;
    public CanvasGroup configScreenMax;
    public CanvasGroup configScreenMin;
    public CanvasGroup checkmarkMax;
    public CanvasGroup checkmarkMin;
    public CanvasGroup buttonContinue;

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

    private void Start()
    {
        gameManager = GameManager.Instance;
        currentScreen = Screen.Main;

        //> Forces the following elements to be transparent (and unclickable) on start
        checkmarkMax.alpha = 0;
        checkmarkMin.alpha = 0;
        UiUtil.Instance.Toggle(buttonContinue, false);
        UiUtil.Instance.Toggle(configScreenMax, false);
        UiUtil.Instance.Toggle(configScreenMin, false);

        UiUtil.Instance.Toggle(loadingScreen, true);

        if (DebugDeletePlayerPrefs)
            PlayerPrefs.DeleteAll();
        
        //> Check for pre-existing save data in PlayerPrefs
        if (StorageManager.northMin != -1 && StorageManager.eastMin != -1 && StorageManager.southMin != -1 && StorageManager.westMin != -1)
        {
            gameManager.North.lowestValue = (int)StorageManager.northMin;
            gameManager.East.lowestValue  = (int)StorageManager.eastMin;
            gameManager.South.lowestValue = (int)StorageManager.southMin;
            gameManager.West.lowestValue  = (int)StorageManager.westMin;
            isCalibratedMin = true;
        }
        if (StorageManager.northMax != -1 && StorageManager.eastMax != -1 && StorageManager.southMax != -1 && StorageManager.westMax != -1)
        {
            gameManager.North.highestValue = (int)StorageManager.northMax;
            gameManager.East.highestValue  = (int)StorageManager.eastMax;
            gameManager.South.highestValue = (int)StorageManager.southMax;
            gameManager.West.highestValue  = (int)StorageManager.westMax;
            isCalibratedMax = true;
        }
        
    }

    private void Update()   //< It would be better if I used a state machine for this.
    {
        switch (currentScreen)
        {
            case Screen.CalibrationMax:
                gameManager.CalibrateSensors();
                maxNorth.text = gameManager.North.highestValue.ToString();
                maxEast.text = gameManager.East.highestValue.ToString();
                maxSouth.text = gameManager.South.highestValue.ToString();
                maxWest.text = gameManager.West.highestValue.ToString();
                if (!isCalibratedMax)
                {
                    isCalibratedMax = true;
                }
                break;
            case Screen.CalibrationMin:
                gameManager.CalibrateSensors();
                minNorth.text = gameManager.North.lowestValue.ToString();
                minEast.text = gameManager.East.lowestValue.ToString();
                minSouth.text = gameManager.South.lowestValue.ToString();
                minWest.text = gameManager.West.lowestValue.ToString();
                if (!isCalibratedMin)
                    isCalibratedMin = true;
                break;
            case Screen.Main:
                if (isCalibratedMax && checkmarkMax.alpha != 0.1f)
                    checkmarkMax.alpha = 0.1f;
                if (isCalibratedMin && checkmarkMin.alpha != 0.1f)
                    checkmarkMin.alpha = 0.1f;
                if (isCalibratedMax && isCalibratedMin && buttonContinue.alpha != 1f)
                {
                    UiUtil.Instance.Toggle(buttonContinue, true);
                }
                break;
        }
        //Debug.Log($"Current screen: {currentScreen}");
    }

    //> The following functions will be triggered by buttons
    public void SetCurrentScreenToMax()
    {
        currentScreen = Screen.CalibrationMax;
    }

    public void SetCurrentScreenToMin()
    {
        currentScreen = Screen.CalibrationMin;
    }

    public void SetCurrentScreenToMain()
    {
        currentScreen = Screen.Main;
    }

    public void ResetCalibrationLow()
    {
        gameManager.ResetSensorCalibration(Sensor.CalibrationType.Low);
    }

    public void ResetCalibrationHigh()
    {
        gameManager.ResetSensorCalibration(Sensor.CalibrationType.High);
    }

    public void SaveCalibrationToPlayerPrefs()
    {
        StorageManager.northMin = gameManager.North.lowestValue;
        StorageManager.eastMin  = gameManager.East.lowestValue;
        StorageManager.southMin = gameManager.South.lowestValue;
        StorageManager.westMin  = gameManager.West.lowestValue;

        StorageManager.northMax = gameManager.North.highestValue;
        StorageManager.eastMax  = gameManager.East.highestValue;
        StorageManager.southMax = gameManager.South.highestValue;
        StorageManager.westMax  = gameManager.West.highestValue;
    }

}
