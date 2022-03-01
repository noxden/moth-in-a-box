//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 27-02-22
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

    public Sensor North;
    public Sensor East;
    public Sensor South;
    public Sensor West;

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

        North = new Sensor(SensorName.North);
        East = new Sensor(SensorName.East);
        South = new Sensor(SensorName.South);
        West = new Sensor(SensorName.West);

        if (North != null && East != null && South != null && West != null)
            Debug.Log($"Initialized all Sensors.", this);
        //Cursor.lockState = CursorLockMode.Locked;       //< Locks the mouse to the viewport
    }

    public void SetSensorValues(int valueNorth, int valueEast, int valueSouth, int valueWest)
    {
        North.SetCurrentValue(valueNorth);
        East.SetCurrentValue(valueEast);
        South.SetCurrentValue(valueSouth);
        West.SetCurrentValue(valueWest);

        if (!isBoardConnected)          //< If isBoardConnected is still false, set it true.
            isBoardConnected = true;
        //Debug.Log($"{valueNorth}, {valueEast}, {valueWest}, {valueSouth}");
    }

    public float GetSensorValue(SensorName sensorName)
    {
        switch (sensorName)
        {
            case SensorName.North:
                return North.PercentLight();
            case SensorName.East:
                return East.PercentLight();
            case SensorName.South:
                return South.PercentLight();
            case SensorName.West:
                return West.PercentLight();
            default:
                return 0f;
        }
    }

    public void CalibrateSensors()
    {
        North.Calibrate();
        East.Calibrate();
        South.Calibrate();
        West.Calibrate();
    }

    public void ResetSensorCalibration(Sensor.CalibrationType type)
    {
        North.ResetCalibration(type);
        East.ResetCalibration(type);
        South.ResetCalibration(type);
        West.ResetCalibration(type);
        Debug.Log($"Reset calibration \"{type}\" for all sensors.", this);
    }
}
