using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitAnchor : MonoBehaviour {

	void OnCollisionEnter(Collision _Col)
    {
        GameObject _Obj = _Col.gameObject;
        if(_Obj.CompareTag("Player"))
        {           
            _Obj.transform.parent = transform;
            Camera.main.GetComponentInParent<Follow>().SetStopFollowing(true);
            GetComponent<Rigidbody>().DOMove(transform.position + new Vector3(0f, 100f, 0f), 10f);
            GetComponent<Rigidbody>().useGravity = false;
            SceneHandler.GetSceneHandler().LoadMapScene();         
        }
    }
}
