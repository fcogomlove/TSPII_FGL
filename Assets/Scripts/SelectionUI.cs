using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class SelectionUI : MonoBehaviour
{
    public static bool gazedAt;
    public float fillTime = 3f;
    public Image radialImage;
    public UnityEvent onFillComplete;

    private Coroutine fillCoroutine;

    void Start()
    {
        gazedAt = false;
        radialImage.fillAmount = 0f;
    }

    public void OnPointerEnter()
    {
        gazedAt = true;
        if (fillCoroutine != null)
        {
            //Metodo para el llenado de la imagen
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(FillRadial());
    }

    public void OnPointerExit()
    {
        gazedAt = false;
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
        }

        radialImage.fillAmount = 0f;
    }

    private IEnumerator FillRadial()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillTime)
        {
            if (!gazedAt)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            radialImage.fillAmount = Mathf.Clamp01(elapsedTime/fillTime);

            yield return null;
        }
        onFillComplete?.Invoke();
    }

    void Update()
    {
        
    }
}
