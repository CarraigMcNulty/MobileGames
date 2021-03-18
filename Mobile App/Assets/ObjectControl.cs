using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour , IControlllable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }   
    public void youve_been_touched()
    {
        transform.position += Vector3.down;
    }

    void IControlllable.moveTo(Vector3 destination)
    {
        transform.position = destination;
    }

    public void changeColor(Color color)
    {
        Renderer selectedObjectColour = this.gameObject.GetComponent<Renderer>();
        selectedObjectColour.material.SetColor("_Color", color);
    }
}

 
