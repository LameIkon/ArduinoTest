using UnityEngine;
using System.IO.Ports; // This makes it possible to interface with the ports.

public class CubeController : MonoBehaviour
{
    // IMPORTANT! Close the Serial Monitor window in the Arduino IDE else it will use the port.
    private SerialPort _serial = new SerialPort("COM3", 9600);

    private string _data; // The data from Serial
    private Renderer _cubeRenderer;
    private Color _color;

    void Start()
    {
        _serial.DtrEnable = true; // This needs to be here because else it will not work.
        _serial.Open(); // Opens the serial moniter on the Arduino.
        _serial.ReadTimeout = 100; // Do not know what this is.
        _cubeRenderer = GetComponent<Renderer>();
       // _color = _cubeRenderer.material.color;
    }


    // Update is called once per frame
    void Update()
    {
        _data = _serial.ReadLine(); // Reads the line, this does not need to be in an update, but right now I do not know how to make it an event.
        string[] tokens = _data.Split(','); // Splits the data into tokens.
        int[] tokensInts = new int[tokens.Length]; // Creates an array of ints with the length of the tokens. This can be done better and with less memory, because we know the data coming in.
        for (int i = 0; i < tokens.Length; i++) // Fills the ints array with the strings from tokens.
        {
            tokensInts[i] = int.Parse(tokens[i]);
        }
        Debug.Log(Map(tokensInts[1]));
        _color = new Color(Map(tokensInts[0]), Map(tokensInts[1]), Map(tokensInts[2]), Map(tokensInts[3]));
        _cubeRenderer.material.SetColor("_baseColor", _color);
    }

    private void OnApplicationQuit()
    {
        _serial.Close();  // It is important to close the port again.
    }

    /// <summary>
    /// Maps an integer value to a float between <c>0</c> and <c>1</c>.
    /// </summary>
    /// <param name="value">The value that needs to be changed.</param>
    /// <param name="lowIn">The lowest the value can be.</param>
    /// <param name="highIn">The highst the value can be.</param>
    /// <returns>A float between <c>0</c> and <c>1</c></returns>
    private float MapZeroToOne(int value, int lowIn, int highIn) 
    {        
        return (float)(value - lowIn) / (highIn - lowIn);
    }
    
    /// <summary>
    /// Maps an integer value between <c>0</c> and <c>1023</c> to a float between <c>0</c> and <c>1</c>.
    /// </summary>
    /// <param name="value">The value to be mapped.</param>
    /// <returns>A float between <c>0</c> and <c>1</c></returns>
    private float Map(int value) 
    {
        return MapZeroToOne(value, 0, 1023);
    }

}
