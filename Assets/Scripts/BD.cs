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
        Debug.Log("Dato Escuela actualizado: UNAM");
        reference.Child("Escuela").SetValueAsync("UNAM");
        Debug.Log("Dato Año actualizado: 2025");
        reference.Child("Año").SetValueAsync(2025);

        Debug.Log("Dato Escuela actualizado: TEC");
        reference.Child("Escuela").SetValueAsync("TEC");
        Debug.Log("Dato Año actualizado: 2026");
        reference.Child("Año").SetValueAsync(2026);

        //Registro de objeto tipo Usuario
        Usuario usuarioN = new Usuario("Jacobo", "jac@gmail.com");

        string json = JsonUtility.ToJson(usuarioN);
        reference.Child("Usuario1").SetRawJsonValueAsync(json);


    }

    public void LoadFromBD()
    {

        //Carga de valor individual
        reference.Child("Año").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al obtener el valor de la base de datos: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string value = snapshot.Value.ToString();
                    Debug.Log("Tipo de valor obtenido: " + snapshot.Value.GetType());
                    Debug.Log(value);
                }
            }
            else
            {
                Debug.Log("Registro no encontrado");
            }
        });

        //Carga de valores anidados
        reference.Child("Nombre").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al obtener el valor de la base de datos: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot2 = task.Result;
                if (snapshot2.Exists)
                {
                    //Recorrer todos los hijos de Nombre y obtener los valores

                    foreach (DataSnapshot childSnapshot in snapshot2.Children)
                    {
                        string value2 = childSnapshot.Value.ToString();
                        Debug.Log("Tipo de valor obtenido: " + childSnapshot.Value.GetType());
                        Debug.Log(value2);
                    }

                }
            }
            else
            {
                Debug.Log("Registro no encontrado");
            }
        });

        //Carga de valores tipo JSON
        reference.Child("Usuario1").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al obtener el valor de la base de datos: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot3 = task.Result;
                if (snapshot3.Exists)
                {
                    //Convertir el JSON a un diccionario

                    Dictionary<string, object> userData = JsonConvert.DeserializeObject<Dictionary<string, object>>(snapshot3.GetRawJsonValue());
                    Debug.Log(userData.GetType());
                    string nombre = (string)userData["UserName"];
                    string email = (string)userData["Email"];

                    Debug.Log($"Nombre de usuario: {nombre}, Correo: {email}");

                }
            }
            else
            {
                Debug.Log("Registro no encontrado");
            }
        });

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public class Usuario
{
    public string UserName;
    public string Email;

    public Usuario(string userName, string email)
    {
        this.UserName = userName;
        this.Email = email;
    }
}