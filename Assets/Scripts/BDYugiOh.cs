using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using TMPro;

public class BDYugiOh : MonoBehaviour
{
    public DatabaseReference reference;

    [SerializeField]
    TMP_InputField cardNameInput;
    [SerializeField]
    TMP_InputField attackInput;
    [SerializeField]
    TMP_InputField defenseInput;
    [SerializeField]
    TMP_InputField spellTrapInput;
    [SerializeField]
    Toggle isMonsterToggle;
    [SerializeField]
    BDYugiOh yugiOhCards;

    public bool isMonster = true;
    public string cardName;
    public int attack;
    public int defense;
    public string spellTrap;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Start()
    {
        cardNameInput.text = "";
        attackInput.text = "";
        defenseInput.text = "";
        spellTrapInput.text = "";
        isMonster = true;
        isMonsterToggle.isOn = true;

        spellTrapInput.gameObject.SetActive(false);

        attackInput.gameObject.SetActive(true);
        defenseInput.gameObject.SetActive(true);
    }

    public void Booleano(bool toogleB)
    {
        isMonster = toogleB;

        if (isMonsterToggle)
        {
            spellTrapInput.gameObject.SetActive(false);
            attackInput.gameObject.SetActive(true);
            defenseInput.gameObject.SetActive(true);
        }
        else
        {
            spellTrapInput.gameObject.SetActive(true);
            attackInput.gameObject.SetActive(false);
            defenseInput.gameObject.SetActive(false);
        }
    }

    public void RegisterCard()
    {
        cardName = cardNameInput.text;
        isMonster = isMonsterToggle.isOn;

        if (string.IsNullOrEmpty(cardName))
        {
            Debug.LogError("Nombre es necesario para el registro");
            return;
        }

        Dictionary<string, object> cardData = new Dictionary<string, object>();
        cardData["TipoMonstruo"] = isMonster;

        if (isMonster)
        {
            attack = int.Parse(attackInput.text);
            defense = int.Parse(defenseInput.text);

            cardData["ATK"] = attack;
            cardData["DEF"] = defense;
        }
        else
        {
            spellTrap = spellTrapInput.text;
            cardData["Tipo"] = spellTrap;
        }

        reference.Child("Cartas").Child(cardName).SetValueAsync(cardData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
               Debug.Log("Carta registrada: ");
                yugiOhCards.Start();
            }
            else
            {
                Debug.LogError("Error " + task.Exception);
            }
        });
    }

    public void LoadARScence()
    {
        SceneManager.LoadScene("Practica2");
    }

    public void LoadCardData(string cardNameAR, TMP_Text textMesh)
    {          
        reference.Child("Cartas").Child(cardNameAR).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.Exists)
            {
                bool isMonster = bool.Parse(snapshot.Child("TipoMonstruo").Value.ToString());
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
