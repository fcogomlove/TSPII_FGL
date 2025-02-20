using UnityEngine;

public class Colliders : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision con: " + collision.gameObject.name);
    }

    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("Mantiene la colision con : " + collision.gameObject.name);
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("Sale de colision con : " + collision.gameObject.name);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger con : " + other.gameObject.name);
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger con : " + other.gameObject.name);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger con : " + other.gameObject.name);
    }
}
