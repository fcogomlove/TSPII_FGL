using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using System.IO;

public class FlightThreadNoSync : MonoBehaviour
{
    [Header("Velocidad")]
    public float speed = 50f;
    public float rotationSpeed = 100f;

    [Header("")]
    public Transform cameraTransform;
    public Vector2 movementInput;

    //Control de iteraciones
    public int turbulenceIterations = 1000000; //Modificar para Actividad 2

    //Lista de vectores de posición calculados
    private List<Vector3> turbulenceForces = new List<Vector3>();

    //Variables para manipular el hilo secundario
    private Thread turbulenceThread;
    private bool isTurbulenceRunning = false;
    private bool stopTurbulenceThread = false;
    private float capturedTime;

    //Bandera de control sobre lectura

    public bool read = false;
    string filePath;

    public void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void Start()
    {
        filePath = Application.dataPath + "/TurbulenceData.txt";
        Debug.Log("Ruta del archivo: " + filePath);
    }

    void Update()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("No hay camara asignada");
            return;
        }

        //Tiempo transcurrido
        capturedTime = Time.time;

        //Proceso pesado
        if (!isTurbulenceRunning)
        {
            isTurbulenceRunning = true;
            stopTurbulenceThread = false;

            //Hilo Secundario
            turbulenceThread = new Thread(() => SimulateTurbulence(capturedTime));
            turbulenceThread.Start();
        }

        //Mover la nave linealmente
        Vector3 moveDirection = cameraTransform.forward * movementInput.y * speed * Time.deltaTime;
        this.transform.position += moveDirection;

        //Mover la nave en rotación
        float yaw = movementInput.x * rotationSpeed * Time.deltaTime;
        this.transform.Rotate(0, yaw, 0);

        //Metodo para lectura del archivo
        TryReadFile();

    }

    //Actividad 1
    public void SimulateTurbulence(float time)
    {
        turbulenceForces.Clear();

        //Repeticiones
        for (int i = 0; i < turbulenceIterations; i++)
        {
            //Verificar si se debe detener el hilo
            if (stopTurbulenceThread)
            {
                break;
            }

            Vector3 force = new Vector3(
                    Mathf.PerlinNoise(i * 0.001f, time) * 2 - 1,
                    Mathf.PerlinNoise(i * 0.002f, time) * 2 - 1,
                    Mathf.PerlinNoise(i * 0.003f, time) * 2 - 1
                );
            turbulenceForces.Add(force);
        }

        //Señal en consola de inicio del hilo
        Debug.Log("Iniciando simulación de turbulencia");

        //Escritura del archivo
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var force in turbulenceForces)
            {
                writer.WriteLine(force.ToString());
            }

            writer.Flush();
        }

        Debug.Log("Archivo escrito");

        //Simulacion completada
        isTurbulenceRunning = false;
    }

    void TryReadFile()
    {
        try
        {
            string content = File.ReadAllText(filePath);

            Debug.Log("Archivo leido: " + content);
        }
        catch (IOException ex)
        {
            Debug.Log("Error de acceso al archivo: " + ex.Message);
        }
    }

    private void OnDestroy()
    {
        //Inidcamos el cierre del hilo secundario
        stopTurbulenceThread = true;

        //Verificamos si el hilo existe y se está ejecutando
        if (turbulenceThread != null && turbulenceThread.IsAlive)
        {
            //Lo unimos al hilo principal y cerramos ejecucion
            turbulenceThread.Join();
        }
    }
}
