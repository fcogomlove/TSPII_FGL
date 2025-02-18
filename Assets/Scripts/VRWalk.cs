using UnityEngine;

public class VRWalk : MonoBehaviour
{
    public Transform vrCamera;
    public float toogleAngle = 30.0f;
    public float speed = 3.0f;
    public bool moveForward;

    private CharacterController cc;

    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if(vrCamera.eulerAngles.x >= toogleAngle && vrCamera.eulerAngles.x < 90.0f)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if(moveForward)
        {
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);
        }
    }
}
