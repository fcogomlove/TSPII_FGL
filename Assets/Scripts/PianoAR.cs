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

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                btnName = hit.transform.name;
                switch (btnName)
                {
                    case "Do":
                        audioSource.clip = audioClips[0];
                        audioSource.Play();
                        break;
                    case "Re":
                        audioSource.clip = audioClips[1];
                        audioSource.Play();
                        break;
                    case "Mi":
                        audioSource.clip = audioClips[2];
                        audioSource.Play();
                        break;
                    case "Fa":
                        audioSource.clip = audioClips[3];
                        audioSource.Play();
                        break;
                    case "Sol":
                        audioSource.clip = audioClips[4];
                        audioSource.Play();
                        break;
                    case "La":
                        audioSource.clip = audioClips[5];
                        audioSource.Play();
                        break;
                    case "Si":
                        audioSource.clip = audioClips[6];
                        audioSource.Play();
                        break;
                    default:
                        Debug.Log("Hit en algun otro elemento");
                        break;
                }
            }
        }else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                btnName = hit.transform.name;
                switch (btnName)
                {
                    case "Do":
                        audioSource.clip = audioClips[0];
                        audioSource.Play();
                        break;
                    case "Re":
                        audioSource.clip = audioClips[1];
                        audioSource.Play();
                        break;
                    case "Mi":
                        audioSource.clip = audioClips[2];
                        audioSource.Play();
                        break;
                    case "Fa":
                        audioSource.clip = audioClips[3];
                        audioSource.Play();
                        break;
                    case "Sol":
                        audioSource.clip = audioClips[4];
                        audioSource.Play();
                        break;
                    case "La":
                        audioSource.clip = audioClips[5];
                        audioSource.Play();
                        break;
                    case "Si":
                        audioSource.clip = audioClips[6];
                        audioSource.Play();
                        break;
                    default:
                        Debug.Log("Hit en algun otro elemento");
                        break;
                }
            }
        }
    }
}
