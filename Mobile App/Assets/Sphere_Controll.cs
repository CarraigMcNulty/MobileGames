using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Controll : MonoBehaviour, IControlllable
{
    Vector3 drag_position;

    public void moveTo(Vector3 destination)
    {
         drag_position = destination;
    }

    public void youve_been_touched()
    {
        transform.position += Vector3.down;
    }

    // Start is called before the first frame update
    void Start()
    {
        drag_position = transform.position;
    }

    public void changeColor(Color color)
    {
        Renderer selectedObjectColour = this.gameObject.GetComponent<Renderer>();
        selectedObjectColour.material.SetColor("_Color", color);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.5f);
    }
}
