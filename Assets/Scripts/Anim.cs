using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anim : MonoBehaviour
{
    public Animator peasantAnim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        peasantAnim = this.GetComponent<Animator>();
    }

    public void PlayAnimPeasant()
    {
        peasantAnim.enabled = true;
        peasantAnim.Play("Peasant");
    }

    public void StopAnimPeasant()
    {
        peasantAnim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
