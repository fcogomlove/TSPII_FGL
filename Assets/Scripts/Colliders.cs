using UnityEngine;

public class Colliders : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision con: " + collision.gameObject.name);
    }
}
