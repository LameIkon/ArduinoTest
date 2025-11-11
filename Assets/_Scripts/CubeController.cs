using UnityEngine;
using System.IO.Ports;

public class CubeController : MonoBehaviour
{
    private SerialPort _serial = new SerialPort("COM3", 9600);

    private string _data;

    void Start()
    {
        _serial.DtrEnable = true;
        _serial.Open();
        _serial.ReadTimeout = 100;
    }


    // Update is called once per frame
    void Update()
    {
        _data = _serial.ReadLine();
        Debug.Log(_data);
    }

    private void OnApplicationQuit()
    {
        _serial.Close();   
    }

}
