using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour {

    //debug thing
    //public GameObject thing;
    //transform for other scripts
    //return the position in world floor surface that the mouse is at
    [Header("Floor layer mask")]
    public static int layerMask = 256;

    private static Vector3 lastPos;

    static public Vector3 GetWorldMousePos()
    {
        //raycast stuff
        RaycastHit rayHit; //ray hit info
        //send a ray from the position of mouse on screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMask))
        //if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
            //dubug spawn object
            //GameObject clone = thing;
            //Instantiate(clone, rayHit.point, Quaternion.identity);
            lastPos = rayHit.point;
            //set the mouse screen to world pos
            return rayHit.point;
        }
        return lastPos;
    }

    static public RaycastHit GetWorldMouseHit()
    {
        RaycastHit rayHit; //ray hit info
        //send a ray from the position of mouse on screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out rayHit, Mathf.Infinity);
        
        return rayHit;
    }

    static public RaycastHit GetWorldMouseHit(int _LayerMask)
    {
        RaycastHit rayHit; //ray hit info
        //send a ray from the position of mouse on screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out rayHit, Mathf.Infinity, _LayerMask);

        return rayHit;
    }
}
