using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EyeBallController : MonoBehaviour {

    public bool TrackPlayer = false;
    public bool TrackMouse = false;
    public float RayDistance = 10f;

    public Vector2 XRotationRange;
    public Vector2 YRotationRange;
    public Vector2 DelayRange;
    public float TransitionDuration;

    private float LastTime;
    private float Delay;
	
	// Update is called once per frame
	void Update () {
        if(TrackMouse)
        {
            RotateAtMouse();
        }
        else if (TrackPlayer)
        {
            RotateAtPlayer();
        }
        else if(Time.time - LastTime > Delay)
        {
            RotateEye();
        }
        
	}

    private void RotateEye()
    {
        float _X = Random.Range(XRotationRange.x, XRotationRange.y);
        float _y = Random.Range(YRotationRange.x, YRotationRange.y);
        Vector3 NewRotation = new Vector3(_X, _y, 90);
        transform.DOLocalRotate(NewRotation, TransitionDuration);
        Delay = Random.Range(DelayRange.x, DelayRange.y);
        LastTime = Time.time;
    }

    private void RotateAtMouse()
    {
        RaycastHit _hitInfo;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hitInfo, RayDistance))
        {
            transform.DOLookAt(_hitInfo.point, TransitionDuration);
        }
    }

    private void RotateAtPlayer()
    {
        transform.DOLookAt(PlayerManager.GetInstance().Player.transform.position, 1f);
    }

    void OnMouseDown()
    {
        TrackMouse = true;
    }
}
