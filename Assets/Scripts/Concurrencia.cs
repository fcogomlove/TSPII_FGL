using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class Concurrencia : MonoBehaviour
{
    [Header("Movimiento")]
    public float desplazamiento = 100;
    public float rotacionCubo = 100;
    public float moveSincrono = 0.05f;
    public float moveThread = 0.05f;
    public float moveTask = 0.05f;
    public float moveCoroutine = 0.05f;

    [Header ("Activa los metodos")]
    public bool useSincrono;
    public bool useThread;
    public bool useTask;
    public bool useCoroutine;

    [Header("Esferas a mover")]
    public Transform sincronoSphere;
    public Transform threadSphere;
    public Transform taskSphere;
    public Transform coroutineSphere;
    public Transform mainCube;

    //Accion a ejecutar en hilo secundario

    private Queue<Action> mainThreadActions = new Queue<Action>();

    void Start()
    {
        if (useSincrono) MoveSincrono();
        if (useThread) MoveWithThread();
        if (useTask) MoveWithTask();
        if (useCoroutine) StartCoroutine(MoveWithCoroutine()); //Modificar con la llamada de la corutina
    }

    void Update()
    {
        //Siempre gira el cubo de referencia
        mainCube.Rotate(Vector3.up, rotacionCubo * Time.deltaTime);

        //Ejecuta las acciones en el hilo principal

        lock (mainThreadActions)
        {
            while (mainThreadActions.Count > 0)
            {
                mainThreadActions.Dequeue().Invoke();
            }
        }

    }

    // Metodo sincrono
    void MoveSincrono()
    {
        for (int i = 0; i <= desplazamiento; i++)
        {
            sincronoSphere.position += Vector3.right * moveSincrono;
        }

        //Delay en el hilo principal
        Thread.Sleep(50);
    }

    //Metodo con hilo secundario
    private void MoveWithThread()
    {
        new Thread(() =>
        {
            for (int i = 0; i <= desplazamiento; i++)
            {
                Thread.Sleep(50);

                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        threadSphere.position += Vector3.right * moveThread;
                    });
                }
            }
        }).Start();
    }

    //Metodo con task
    private async void MoveWithTask()
    {
        await Task.Run(() =>
        {
            for (int i = 0; i <= desplazamiento; i++)
            {
                Thread.Sleep(50);

                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        taskSphere.position += Vector3.right * moveTask;
                    });
                }
            }
        });
    }

    //Metodo con corutina
    IEnumerator MoveWithCoroutine()
    {
        for(int i = 0; i <= desplazamiento; i++)
        {
            coroutineSphere.position += Vector3.right * moveCoroutine;

            yield return new WaitForSeconds(moveCoroutine);
        }
    }
}
