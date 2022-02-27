//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 25-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SensorName
{
    North, East, South, West
}

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public bool isBoardConnected = false;

    [SerializeField]
    private int SensorNorth;
    [SerializeField]
    private int SensorEast;
    [SerializeField]
    private int SensorSouth;
    [SerializeField]
    private int SensorWest;
    #region Archived
    /*
    //> These sensor values can only be set using the "SetSensorValues" function
    public float SensorNorth { private set; get; }
    public float SensorEast { private set; get; }
    public float SensorSouth { private set; get; }
    public float SensorWest { private set; get; }
    */
    #endregion

    private void Awake()    //Awake() is run even before Start()
    {
        if (Instance == null)   //< With this if-structure it is IMPOSSIBLE to create more than one instance
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //< Referring to the gameObject, this singleton script (class) is attached to
        }
        else
        {
            Destroy(this.gameObject);   //< If you somehow still get to create a new singleton gameobject regardless, destroy the new one
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        //Cursor.lockState = CursorLockMode.Locked;       //< Locks the mouse to the viewport
    }

    public void SetSensorValues(int valueNorth, int valueEast, int valueSouth, int valueWest)
    {
        SensorNorth = valueNorth;
        SensorEast = valueEast;
        SensorWest = valueWest;
        SensorSouth = valueSouth;

        if (!isBoardConnected)          //< If isBoardConnected is still false, set it true.
            isBoardConnected = true;
        //Debug.Log($"{SensorNorth}, {SensorEast}, {SensorWest}, {SensorSouth}");
    }

    public int GetSensorValue(SensorName lightSensor)
    {
        switch (lightSensor)
        {
            case SensorName.North:
                return SensorNorth;
            case SensorName.East:
                return SensorEast;
            case SensorName.South:
                return SensorSouth;
            case SensorName.West:
                return SensorWest;
            default:
                return 0;
        }
    }
}