using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlllable 
{              
     void youve_been_touched();
     void moveTo(Vector3 destination);

    void changeColor(Color color);

    GameObject gameObject { get; }
}
