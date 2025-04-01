using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class FlightVR : MonoBehaviour
{
    [Header("Velocidad")]
    public float speed = 50f;
    public float rotationSpeed = 100f;
    //Velocidad base de movimiento
    public float baseFloatSpeed = 2f;

    [Header("")]
    public Transform cameraTransform;
    public Vector2 movementInput;

    //Control de iteraciones
    public int turbulenceIterations = 1000000; //Modificar para Actividad 2

    //Intensidad en el tambaleo
    public float rollIntensity = 10f;

    //Lista de vectores de posición calculados
    private List<Vector3> turbulenceForces = new List<Vector3>();

    //Vector unitario de fuerzas
    private Vector3 currentTurbulence = Vector3.zero;
    private object turbulenceLock = new object(); //evitar accesos simultaneos

    //Variables para manipular el hilo secundario
    private Thread turbulenceThread;
    private bool isTurbulenceRunning = false;
    private bool stopTurbulenceThread = false;
    private float capturedTime;

    public void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void Start()
    {

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

        //Obtener el vector de turbulencia
        lock (turbulenceLock)
        {
            if (turbulenceForces.Count > 0)
            {
                currentTurbulence = turbulenceForces[turbulenceForces.Count - 1] * 0.1f;
            }
        }

        //Movimiento base
        Vector3 floatDirection = cameraTransform.forward * baseFloatSpeed * Time.deltaTime;

        //Mover la nave linealmente
        Vector3 moveDirection = cameraTransform.forward * movementInput.y * speed * Time.deltaTime;
        this.transform.position += moveDirection + floatDirection + currentTurbulence;

        //Mover la nave en rotación
        float yaw = movementInput.x * rotationSpeed * Time.deltaTime;
        Vector3 turbulenceRotation = new Vector3(currentTurbulence.x, 0, currentTurbulence.z) * 5f;
        float roll = currentTurbulence.y * rollIntensity;
        this.transform.Rotate(turbulenceRotation + new Vector3(0,yaw,roll));

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

        //Simulacion completada
        isTurbulenceRunning = false;
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
