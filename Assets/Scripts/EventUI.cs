using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class EventUI : MonoBehaviour
{
    private int currentIndex = 0;

    public List<string> mensajes;
    public TextMeshProUGUI texto;

    void Start()
    {
        
    }

    public void ChangeScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeScenebyIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReloadCurrentScene()
    {
        Scene current = SceneManager.GetActiveScene();

        SceneManager.LoadScene(current.name);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void CycleText()
    {
        currentIndex = (currentIndex + 1) % mensajes.Count;
        UpdateText();
    }

    public void UpdateText()
    {
        if(mensajes.Count > 0 && texto != null)
        {
            texto.text = mensajes[currentIndex];
        }
    }

    void Update()
    {
        
    }
}
