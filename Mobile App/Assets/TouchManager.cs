using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    GameObject ourCameraPlane;
    IControlllable selectedObject;
    Quaternion startOrientation;
    Vector3 scale;
    bool hasMoved = false;
    float startDistance;
    float startAngle;   
 
    // Start is called before the first frame update
    void Start()
    {
         ourCameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
         ourCameraPlane.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
         ourCameraPlane.transform.up = (Camera.main.transform.position - ourCameraPlane.transform.position).normalized;   

        //third method
        ourCameraPlane.transform.eulerAngles = new Vector3 (ourCameraPlane.transform.eulerAngles.x + 42,
                                                ourCameraPlane.transform.eulerAngles.y,
                                                ourCameraPlane.transform.eulerAngles.z);

        ourCameraPlane.transform.position = new Vector3(ourCameraPlane.transform.position.x,
                                                ourCameraPlane.transform.position.y -1.1f,
                                                ourCameraPlane.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)  
        {
            Ray ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit info;

            Debug.DrawRay(ourRay.origin, 30 * ourRay.direction, Color.red);

            if (Physics.Raycast(ourRay, out info))
            {
                IControlllable object_hit = info.transform.GetComponent<IControlllable>();

                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:
                        break;

                    case TouchPhase.Moved:
                        if (selectedObject != null)
                        {
                            Ray new_positional_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                            //First Movement Type
                            //start_distance_to_object = Vector3.Distance(Camera.main.transform.position, info.transform.position);
                            //selectedObject.moveTo(new_positional_ray.GetPoint(start_distance_to_object));

                            //Second Movement Type
                            if (info.transform == ourCameraPlane.transform)
                            {
                                Vector3 plane_hit = info.point;
                                selectedObject.moveTo(plane_hit);
                            }

                            hasMoved = true;
                        }

                        break;

                    case TouchPhase.Ended:

                        if (hasMoved == true)
                        {
                            print("no tap");
                            hasMoved = false;
                        }
                        
                        else if (hasMoved == false)
                        {
                            print("tap");

                            if (object_hit != null)
                            {
                                if (selectedObject != null)
                                {

                                    selectedObject.changeColor(Color.grey);
                                }

                                if (object_hit == selectedObject)
                                { object_hit.youve_been_touched();}
                                
                                selectedObject = object_hit;
                                selectedObject.changeColor(Color.red);
                                print("Object Selected");
                            }

                            else if (selectedObject != null && object_hit == null)
                            {   
                                selectedObject.changeColor(Color.grey);
                                selectedObject = null;
                                print("Deselect Object");
                            } 
                        }
                        break;
                }    
            }
        }

        else if (Input.touchCount == 2)
        {      
            if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
               
                startDistance = Vector3.Distance(touch0, touch1);
                startAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);

                if (selectedObject != null)
                {
                    startOrientation = selectedObject.gameObject.transform.rotation;
                    scale = selectedObject.gameObject.transform.localScale;
                }

                else
                {
                    startOrientation = Camera.main.transform.rotation;
                    scale = Camera.main.transform.localScale;
                }
            }

            else if(Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
            {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                float endDistance = Vector2.Distance(touch0, touch1);
                float diff = endDistance/startDistance;
                float latestAngle = Mathf.Atan2(touch1.x - touch0.x, touch1.y - touch0.y);
                float actualAngle = latestAngle - startAngle;
                float rotationDegrees = Mathf.Rad2Deg * actualAngle;

                if (selectedObject == null)
                {
                    if (Quaternion.Angle(startOrientation, startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward)) > 15)
                    {
                        print("Rotate camera");
                        Camera.main.transform.rotation = startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward);
                    }

                    else
                    {
                         print("Scale camera");
                         diff = endDistance - startDistance;
                         Camera.main.transform.position += (diff/1000) * transform.forward;
                    }
                }

                else
                {
                    if (Quaternion.Angle(startOrientation, startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward)) > 15)
                    {
                        print("Rotate object");
                        selectedObject.gameObject.transform.rotation = startOrientation * Quaternion.AngleAxis(rotationDegrees, Camera.main.transform.forward);
                    }

                    else
                    {
                        print("Scale Object");
                        selectedObject.gameObject.transform.localScale = scale * diff;
                    } 
                }
            }
        }
    }
}
