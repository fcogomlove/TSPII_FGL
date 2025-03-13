using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class FlightThread : MonoBehaviour
{
    [Header("Velocidad")]
    public float speed = 50f;
    public float rotationSpeed = 100f;

    [Header("")]
    public Transform cameraTransform;
    public Vector2 movementInput;

    //Control de iteraciones
    public int turbulenceIterations = 1000000;

    //Lista de vectores de posición calculados
    private List<Vector3> turbulenceForces = new List<Vector3>();

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

            //turbulenceThread = new Thread(() => SimulateTurbulence()).Start();
        }

        //Mover la nave linealmente
        Vector3 moveDirection = cameraTransform.forward * movementInput.y * speed * Time.deltaTime;
        this.transform.position += moveDirection;

        //Mover la nave en rotación
        float yaw = movementInput.x * rotationSpeed * Time.deltaTime;
        this.transform.Rotate(0, yaw, 0);

    }

    public void SimulateTurbulence(float time)
    {
        turbulenceForces.Clear();

        //Repeticiones
        for (int i = 0; i < turbulenceIterations; i++)
        {
            Vector3 force = new Vector3(
                    Mathf.PerlinNoise(i * 0.001f, time) * 2 - 1,
                    Mathf.PerlinNoise(i * 0.002f, time) * 2 - 1,
                    Mathf.PerlinNoise(i * 0.003f, time) * 2 - 1
                );
        }
    }
}
