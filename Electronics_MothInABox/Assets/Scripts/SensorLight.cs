//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 27-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLight : LightSource
{
    [Tooltip("Select which real life photoresistor will be connected to this LightSource.")]
    public SensorName lightSensor = SensorName.North;

    private GameManager gameManager;
    //private PlayerCharacter player;   // is inherited now

    private float baseStrength;

    private new void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;

        player = FindObjectOfType<PlayerCharacter>();   //< Is actually slower and more performance ineffective than GameObject.Find, but makes sure that it has the required component.
        followPlayer();

        // Set up SonsorLight variables
        baseStrength = strength;
    }

    private void Update()
    {
        followPlayer();

        if (!gameManager.isBoardConnected)
            return;

        //> These could also be called in OnTriggerStay
        ModifyStrengthWithSensorPercentage();
    }

    public void ModifyStrengthWithSensorPercentage()  //< Basically just a guard clause function
    {
        float currentPercentage = gameManager.GetSensorValue(lightSensor);  //< Asks GameManager for a percentageValue and uses its own lightSensor name as key.

        strength = baseStrength * currentPercentage;
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