//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLight : LightSource
{
    [Tooltip("Select which real life photoresistor will be connected to this LightSource.")]
    public SensorName lightSensor = SensorName.North;

    private GameManager gameManager;
    private PlayerCharacter player;

    [SerializeField]
    private int currentValue;
    [SerializeField]
    private int highestValue;
    [SerializeField]
    private int lowestValue;

    private float baseStrength;
    private float strengthPercentage;

    private new void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
        player = FindObjectOfType<PlayerCharacter>();   //< Is actually slower and more performance ineffective than GameObject.Find, but makes sure that it has the required component.
        followPlayer();

        // Set up SonsorLight variables
        highestValue = 0;
        lowestValue = 1023;
        baseStrength = strength;
    }

    private void Update()
    {
        followPlayer();

        if (!gameManager.isBoardConnected)
            return;

        FetchSensorValue();
        Calibrate(currentValue);    //< Recalibrates constantly during runtime
        ConvertSensorDataToStrength(currentValue);
        ApplyStrengthPercentage();
    }

    public void FetchSensorValue()  //< Basically just a guard clause function
    {
        int newSensorValue = gameManager.GetSensorValue(lightSensor);
        if (currentValue == newSensorValue)    //< Guard clause
            return;

        currentValue = newSensorValue;
    }

    public void Calibrate(int value)
    {
        if (value > highestValue)
            highestValue = value;
        if (value < lowestValue)
            lowestValue = value;
    }

    public void ConvertSensorDataToStrength(int value)
    {
        int range = highestValue - lowestValue;
        strengthPercentage = (float)(value - lowestValue) / Mathf.Clamp(range, 1, 1023); 
        //< We need the (float) to prevent an implicit type conversion, which would round the result: https://stackoverflow.com/questions/39773799/unity-debug-log-full-float

        //Debug.Log($"{this.name}'s strengthPercentage: {Mathf.RoundToInt(100*strengthPercentage)}%", this);    
        //< TIL, you can add a context object at the end of Debug.Log to make it easier to find the culprit: https://docs.unity3d.com/ScriptReference/Debug.Log.html
        //< Watch out! Mathf.Round will always round to the neares even number when on ".5": https://forum.unity.com/threads/mathf-roundtoint.491182/
    }

    private void ApplyStrengthPercentage()
    {
        strength = baseStrength * strengthPercentage;
    }

    private void followPlayer()
    {
        //> Guard clause
        if (player == null)     
        {
            Debug.LogWarning($"{this.name} could not find object of type \"PlayerCharacter\".", this);
            return;
        }

        Vector3 pos = this.transform.position;
        Vector3 playerPos = player.transform.position;

        switch (lightSensor)
        {
            case SensorName.North:
                pos = playerPos + Vector3.forward;
                break;
            case SensorName.South:
                pos = playerPos + Vector3.back;
                break;
            case SensorName.East:
                pos = playerPos + Vector3.right;
                break;
            case SensorName.West:
                pos = playerPos + Vector3.left;
                break;
            default:
                Debug.LogError($"Impossible case. Variable \"lightSensor\" can never be null.", this);
                return;
        }
        this.transform.position = pos;
    }

    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private new void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

}