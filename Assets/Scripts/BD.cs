using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using TMPro;

public class BD : MonoBehaviour
{
    public DatabaseReference reference;

    [SerializeField]
    TMP_InputField textoNombre;

    [SerializeField]
    TMP_InputField textoEdad;

    public bool registroBooleano = true;

    [SerializeField]
    TMP_Text textoRegistro;

    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Booleano(bool toogleB)
    {
        registroBooleano = toogleB;
    }

    public void Registro()
    {
        //Primer tipo de dato
        //Generar una clave única para el registro
        string keyNombre = reference.Child("Nombre").Push().Key;
        reference.Child("Nombre").Child(keyNombre).SetValueAsync(textoNombre.text);

        //Clave unica para datos individuales
        string keyDatos = reference.Child("Datos").Push().Key;


        //Segundo tipo de dato que es un valor individual string
        reference.Child("Datos").Child(keyDatos).Child("Texto").SetValueAsync("Registro de texto");

        //Tercer tipo de dato que es un valor int
        reference.Child("Datos").Child(keyDatos).Child("Edad").SetValueAsync(int.Parse(textoEdad.text));

        //Cuarto tipo de dato que es un bool
        reference.Child("Datos").Child(keyDatos).Child("Registro").SetValueAsync(registroBooleano);

        //Datos a actualizar
        reference.Child("Escuela").SetValueAsync("UNAM");
        reference.Child("Año").SetValueAsync("2025");

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
