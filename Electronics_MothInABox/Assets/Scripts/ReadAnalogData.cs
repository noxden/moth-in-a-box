//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ReadAnalogData : MonoBehaviour
{
    public UduinoDevice myDevice;
    private string data;

    private void Awake()
    {
        UduinoManager.Instance.OnDataReceived += OnDataReceived;    //< Add "OnDataReceived" to the delegate of the same name in "UduinoManager.Instance".
    }

    void Start()
    {
        /*
        myDevice = UduinoManager.Instance.GetBoard("DanielsUno");
        if (myDevice == null)
        {
            StartCoroutine(AttemptReconnect());
        }
        StartCoroutine(ReadLightSensors());
        */
        StartCoroutine(EstablishConnection());
    }

    IEnumerator EstablishConnection()
    {
        int attemptCount = 0;
        myDevice = UduinoManager.Instance.GetBoard("DanielsUno");
        while (myDevice == null)
        {
            if (attemptCount >= 20)
            {
                Debug.LogError("<color=#FF7F7F> Could not establish connection to board after 20 attempts. Please restart the application. </color>", this);
                Application.Quit();
            }
            else
            {
                Debug.LogWarning("<color=#FF7F7F> Could not establish connection to board. Trying again... </color>", this);
                //UduinoManager.Instance.DiscoverPorts();   //< Leads to faster discover times, but way longer waiting times when closing the game preview
                yield return new WaitForSeconds(2f);
                myDevice = UduinoManager.Instance.GetBoard("DanielsUno");
                attemptCount += 1;
            }
        }
        Debug.LogWarning("<color=#90EE90> Established connection to board. </color>", this);
        StartCoroutine(ReadLightSensors());
    }

    //> This IEnumerator replaces this classes Update() function and only runs every 100 milliseconds.
    IEnumerator ReadLightSensors()
    {
        while (Application.isPlaying)   //< Coroutine will not end while the game is running
        {
            yield return new WaitForSeconds(0.1f);
            UduinoManager.Instance.Read(myDevice, "lightSensor");
        }

        //StartCoroutine(ReadLightSensors());     //< Coroutine restarts itself.
    }

    public void OnDataReceived(string _data, UduinoDevice device)
    {
        if (data == _data)  //< Guard clause to prevent redundant updates. Compares old data with newly receíved data, if they are equal, ...
            return;         //  ... the function stops here.


        //> If the new data is unequal the old data, this function resumes with the following: 
        data = _data;
        #region Debug Message
        //Debug.Log($"Data from Sensors: {data}");
        #endregion

        List<int> iSensorValues = new List<int>();
        string[] sSensorValues = new string[4];

        sSensorValues = data.Split(',');
        foreach (string entry in sSensorValues)
        {
            int tempInt = int.Parse(entry);
            iSensorValues.Add(tempInt);
        }
        GameManager.Instance.SetSensorValues(iSensorValues[0], iSensorValues[1], iSensorValues[2], iSensorValues[3]);
    }
}
