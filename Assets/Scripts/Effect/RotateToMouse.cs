using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float maximumlLengt;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null){
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumlLengt)) {
                    RotateToMouseDtirection(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maximumlLengt);
                RotateToMouseDtirection(gameObject, pos);
            }
            } else {

                Debug.Log("no Camera");
            }
    }
    void RotateToMouseDtirection (GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
}


