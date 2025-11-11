using UnityEngine;
using System.IO.Ports;

public class CubeController : MonoBehaviour
{
    // IMPORTANT! Close the Serial Monitor window in the Arduino IDE else it will use the port.
    private SerialPort _serial = new SerialPort("COM3", 9600);

    private string _data; // The data from Serial

    void Start()
    {
        _serial.DtrEnable = true; // This needs to be here because else it will not work.
        _serial.Open(); // Opens the serial moniter on the Arduino.
        _serial.ReadTimeout = 100; // Do not know what this is.
    }


    // Update is called once per frame
    void Update()
    {
        _data = _serial.ReadLine(); // Reads the line, this does not need to be in an update, but right now I do not know how to make it an event.
        Debug.Log(_data); 
    }

    private void OnApplicationQuit()
    {
        _serial.Close();  // It is important to close the port again.
    }

}
