using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float speed = 10.0f;
    bool activateAccelerometer;

    public void toggleAccelerometer()
    {
        if(activateAccelerometer == false)
        {
            activateAccelerometer = true;
            
        }

        else
        {
            activateAccelerometer = false;
        }
    }

    void Update()
    { 
        if (activateAccelerometer == true)
        {
            Vector3 dir = Vector3.zero;

            // assumimg the device is held parallel to the ground
            // and Home button is in the right hand

            // remap device acceleration axis to game coordinates:
            //  1) XY plane of the device is mapped onto XZ plane
            //  2) rotated 90 degrees around Y axis
            dir.x = -Input.acceleration.y;
            dir.z = Input.acceleration.x;

            // clamp acceleration vector to unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            // Make it move 10 meters per second instead of 10 meters per frame...
            dir *= Time.deltaTime;

            // Move object
            Camera.main.transform.Translate(dir * speed);
        }
            
    }
    
}
