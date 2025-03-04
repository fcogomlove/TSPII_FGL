using UnityEngine;
using UnityEditor.Animations;

public class VRselection : MonoBehaviour
{
    [SerializeField]
    Material inactivo;
    [SerializeField]
    Material activo;
    [SerializeField]
    public AnimatorController obj;

    public bool gazed;

    Renderer rendererObj;

    public GameObject objeto;



    void Start()
    {
        objeto = this.gameObject;
        gazed = false;
        rendererObj = GetComponent<Renderer>();
        //inactivo = gameObject.GetComponent<Material>();

        SetMaterial(gazed);
    }

    public void OnPointerEnter()
    {
        gazed = true;
        SetMaterial(gazed);
    }

    public void OnPointerExit()
    {
        gazed = false;
        SetMaterial(gazed);
    }

    void Update()
    {

    }

    void SetMaterial(bool gazedAt)
    {
        if (inactivo != null && activo != null)
        {
            rendererObj.material = gazedAt ? activo : inactivo;
        }
    }
}
