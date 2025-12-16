using System;
using UnityEngine;
using System.IO.Ports;  // This makes it possible to interface with the ports.
using _Scripts.ScriptableObjects;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class CubeController : MonoBehaviour
{
    // IMPORTANT! Close the Serial Monitor window in the Arduino IDE else it will use the port.
    private SerialPort _serial = new SerialPort("COM3", 9600);

    private string _data; // The data from Serial
    private Renderer _cubeRenderer;
    private Color _color;
    private MeshFilter _meshFilter; // This is the filter that is used to change the mesh of the object.
    [SerializeField] private MeshData _meshData; // The meshes that it change into on a flyweight pattern.
    private int _index = 0; // This is used for the mesh switch.
    
    void Start()
    {
        _serial.DtrEnable = true; // This needs to be here because else it will not work.
        _serial.Open(); // Opens the serial moniter on the Arduino.
        _serial.ReadTimeout = 100; // Do not know what this is.
        _cubeRenderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }


    // Update is called once per frame
    void Update()
    {
        try // try catch because we are reading a line that can return an exception, this is so we can exit gracefully.
        {
            _data = _serial
                .ReadLine(); // Reads the line, this does not need to be in an update, but right now I do not know how to make it an event.
            string[] tokens = _data.Split(','); // Splits the data into tokens.
            int[] tokensInts = new int[tokens.Length]; // Creates an array of ints with the length of the tokens. This can be done better and with less memory, because we know the data coming in.
            for (int i = 0; i < tokens.Length; i++) // Fills the ints array with the strings from tokens.
            {
                tokensInts[i] = int.Parse(tokens[i]);
            }

            // takes the inputs from Serial and maps it to a float between 0 and 1, then makes it into a Color type.
            _color = new Color(Map(tokensInts[0]), Map(tokensInts[1]), 
                                Map(tokensInts[2]), Map(tokensInts[3])); 
            _cubeRenderer.material.color = _color; // Sets the color of the material to the new color.

            if (tokensInts[4] == 0) // if the last token in the _data string is 0, means that the button has been pressed.
            {
                ChangeMesh();
            }
        }
        catch (TimeoutException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (FormatException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void OnApplicationQuit()
    {
        _serial.Close();  // It is important to close the port again.
    }

    /// <summary>
    /// Changes the <c>Mesh</c> of the object with the array <c>Meshes</c>. It does this with a circular array structure.
    /// </summary>
    private void ChangeMesh()
    {
        _index = (_index + 1) % _meshData.Meshes.Length;
        _meshFilter.mesh = _meshData.Meshes[_index];
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
