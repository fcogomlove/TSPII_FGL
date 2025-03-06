using UnityEngine;

public class VRselection : MonoBehaviour
{
    /*[SerializeField]
    Material inactivo;
    [SerializeField]
    Material activo;*/
    [SerializeField]
    public string triggerTag;

    public bool gazed;

    //public GameObject objeto;
    public Animator obj;

    //Renderer rendererObj;



    void Start()
    {
        /*objeto = this.gameObject;
        gazed = false;
        rendererObj = GetComponent<Renderer>();
        //inactivo = gameObject.GetComponent<Material>();*/

        //SetMaterial(gazed);
    }

    public void OnPointerEnter()
    {
        gazed = true;
        //SetMaterial(gazed);
        obj.SetTrigger(triggerTag);
    }

    public void OnPointerExit()
    {
        gazed = false;
        //SetMaterial(gazed);
    }

    void Update()
    {

    }

    /*void SetMaterial(bool gazedAt)
    {
        if (inactivo != null && activo != null)
        {
            rendererObj.material = gazedAt ? activo : inactivo;
        }
    }*/
}
