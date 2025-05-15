using UnityEngine;
using System.IO.Ports;

public class SerialController : MonoBehaviour
{

    public float speed = 5f;
    SerialPort serialPort;
    bool portOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serialPort = new SerialPort("COM7", 9600);
        serialPort.ReadTimeout = 50;

        try
        {
            serialPort.Open();
            portOpen = true;
            Debug.Log("Puerto abierto");

        }
        catch(System.Exception)
        {
            Debug.LogError("Error en la comunicación");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (portOpen)
        {
            try
            {
                string[] data = serialPort.ReadLine().Trim().Split('|');
                float x = float.Parse(data[0]);
                float z = float.Parse(data[1]);

                Debug.Log($"X: {x} Z: {z}");

                Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
                this.transform.Translate(movement);
            }
            catch
            {
                Debug.LogError("Error");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision con " + other.name);

        serialPort.Write("1");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Sale de la colision con " + other.name);

        serialPort.Write("0");
    }
}
