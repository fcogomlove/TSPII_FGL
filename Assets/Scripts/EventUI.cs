using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EventUI : MonoBehaviour
{
    private int currentIndex = 0;

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

    void Update()
    {
        
    }
}
