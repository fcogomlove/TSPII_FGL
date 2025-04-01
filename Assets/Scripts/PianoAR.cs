using UnityEngine;

public class PianoAR : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    string btnName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }
    }
}
