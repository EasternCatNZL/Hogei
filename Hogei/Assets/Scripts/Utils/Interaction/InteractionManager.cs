using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public GameObject MouseFocus;//The Gameobject which the mouse is over
    [Header("Debug Settings")]
    public bool DebugRay = false;
    public GameObject DebugSphere;

	// Update is called once per frame
	void Update () {
        CastRay();
		if(MouseFocus && Input.GetMouseButtonDown(0))
        {
            MouseFocus.GetComponent<Interactable>().MouseClick();
        }
	}

    private void CastRay()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hitInfo;
        if (DebugRay) Debug.DrawRay(_ray.origin, _ray.direction * 50);
        if (Physics.Raycast(_ray, out _hitInfo, 50))
        {
            GameObject obj = _hitInfo.collider.gameObject;
            if(obj.CompareTag("Interactable"))
            {
                if(MouseFocus == null)
                {
                    MouseFocus = obj;
                    MouseFocus.GetComponent<Interactable>().MouseEnter();
                }
            }
            else if (MouseFocus != null)
            {
                MouseFocus.GetComponent<Interactable>().MouseExit();
                MouseFocus = null;
            }
            //Debug
            if (DebugSphere) DebugSphere.transform.position = _hitInfo.point;
        }
        else if (MouseFocus != null)
        {
            MouseFocus.GetComponent<Interactable>().MouseExit();
            MouseFocus = null;
        }

    }
      
}
