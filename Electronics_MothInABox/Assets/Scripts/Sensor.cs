//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 01-03-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor
{
    public enum CalibrationType
    {
        High, Low, Both
    }

    public Sensor(SensorName _name)
    {
        FetchCalibrationFromPlayerPrefs(_name);
    }

    //> Variables
    private const int DEFAULT_high = 0;
    private const int DEFAULT_low = 1023;

    public int currentValue;
    public int highestValue = DEFAULT_high;
    public int lowestValue = DEFAULT_low;

    private float percentageValue;
    //private SensorName name = SensorName.North;

    public void Calibrate()
    {
        if (currentValue > highestValue)
            highestValue = currentValue;
        if (currentValue < lowestValue)
            lowestValue = currentValue;
    }
    //> The following overloads are not used anywhere
    public void Calibrate(CalibrationType type) //< Overload of Calibrate()
    {
        switch (type)
        {
            case CalibrationType.High:
                if (currentValue > highestValue)
                    highestValue = currentValue;
                break;
            case CalibrationType.Low:
                if (currentValue < lowestValue)
                    lowestValue = currentValue;
                break;
            case CalibrationType.Both:
                if (currentValue > highestValue)
                    highestValue = currentValue;
                if (currentValue < lowestValue)
                    lowestValue = currentValue;
                break;
            default:
                break;
        }
    }
    public void Calibrate(CalibrationType type, int value)  //< Overload of Calibrate()
    {
        switch (type)
        {
            case CalibrationType.High:
                if (value > highestValue)
                    highestValue = value;
                break;
            case CalibrationType.Low:
                if (value < lowestValue)
                    lowestValue = value;
                break;
            case CalibrationType.Both:
                if (value > highestValue)
                    highestValue = value;
                if (value < lowestValue)
                    lowestValue = value;
                break;
            default:
                break;
        }
    }

    public void ResetCalibration(CalibrationType type)
    {
        switch (type)
        {
            case CalibrationType.High:
                highestValue = DEFAULT_high;
                break;
            case CalibrationType.Low:
                lowestValue = DEFAULT_low;
                break;
        }
    }

    public void SetCurrentValue(int newValue)
    {
        currentValue = newValue;
    }

    public float PercentLight()
    {
        float range = highestValue - lowestValue;
        percentageValue = (float)(currentValue - lowestValue) / Mathf.Clamp(range, 1, 1023);
        //< We need the (float) to prevent an implicit type conversion, which would round the result: https://stackoverflow.com/questions/39773799/unity-debug-log-full-float
        return percentageValue;
    }

    private void FetchCalibrationFromPlayerPrefs(SensorName name)
    {
        switch (name)
        {
            case SensorName.North:
                if (StorageManager.northMin != -1)
                    highestValue = StorageManager.northMax;
                    lowestValue = StorageManager.northMin;
                break;
            case SensorName.East:
                highestValue = StorageManager.eastMax;
                lowestValue = StorageManager.eastMin;
                break;
            case SensorName.South:
                highestValue = StorageManager.southMax;
                lowestValue = StorageManager.southMin;
                break;
            case SensorName.West:
                highestValue = StorageManager.westMax;
                lowestValue = StorageManager.westMin;
                break;
        }
    }
}
