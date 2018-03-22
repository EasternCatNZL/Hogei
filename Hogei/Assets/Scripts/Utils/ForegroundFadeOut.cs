using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForegroundFadeOut : MonoBehaviour {

    public Transform Target;
    public float FadeDuration;

    private List<GameObject> FadedOutObjects;
    private RaycastHit[] GreatestHits;

    private void Start()
    {
        FadedOutObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        CastRay();
	}

    void CastRay()
    {
        Transform CameraTransform = Camera.main.transform;
        Vector3 Direction = Target.position - CameraTransform.position;
        GreatestHits = Physics.RaycastAll(CameraTransform.position, Direction.normalized, Direction.magnitude/2);
        Debug.DrawLine(CameraTransform.position, CameraTransform.position + Direction.normalized * Direction.magnitude/2);
        FadeOutObject();
        FadeInObjects();
    }

    void FadeOutObject()
    {
        foreach (RaycastHit hit in GreatestHits)
        {
            print("\n" + hit.transform.gameObject.name);
            print(hit.transform.gameObject.layer);
            print(1 << hit.transform.gameObject.layer);
            print(LayerMask.NameToLayer("Enviroment"));
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enviroment"))
            {

                hit.transform.DOScale(new Vector3(0f, 0f, 0f), FadeDuration);
                FadedOutObjects.Add(hit.transform.gameObject);
            }
        }
    }

    void FadeInObjects()
    {
        for(int i = 0; i < FadedOutObjects.Count; ++i)
        {
            if(!GreatestHitsContains(FadedOutObjects[i]))
            {
                FadedOutObjects[i].transform.DOScale(new Vector3(1f, 1f, 1f), FadeDuration);
                FadedOutObjects.RemoveAt(i);
            }
        }
    }

    bool GreatestHitsContains(GameObject _CheckThis)
    {
        foreach (RaycastHit hit in GreatestHits)
        {
            if (_CheckThis == hit.transform.gameObject)
                return true;
        }
        return false;
    }
}
