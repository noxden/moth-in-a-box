using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ReadAnalogData : MonoBehaviour
{
    UduinoDevice myDevcie;

    private void Awake()
    {
        UduinoManager.Instance.OnDataReceived += OnDataReceived;    // Create the Delegate
    }
    // Start is called before the first frame update
    void Start()
    {
        myDevcie = UduinoManager.Instance.GetBoard("DanielsUno");
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame, read the value of the "mySensor" function on our board.
        UduinoManager.Instance.Read(myDevcie, "mySensor");
    }

    public void OnDataReceived(string data, UduinoDevice device)
    {
        Debug.Log(data);    // Use the data as you want!
    }
}
