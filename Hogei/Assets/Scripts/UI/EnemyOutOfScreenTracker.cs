using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOutOfScreenTracker : MonoBehaviour {

    [Header("Camera")]
    [Tooltip("The main camera")]
    public Camera mainCamera;

    [Header("Image")]
    [Tooltip("Indicator object")]
    public GameObject indicatorObject;
    [Tooltip("Indicator image")]
    public Sprite indicator;
    [Tooltip("Offset distance for image along edge of screen")]
    public float imageOffsetDistance = 3.0f;
    [Tooltip("Y plane for indicator")]
    public float yPlane = 2.0f;

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    //player ref
    private GameObject player;

    //control vars
    private bool inView = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }
	
	// Update is called once per frame
	void Update () {
        CheckInCameraView();
        TrackOffScreenTarget();
	}

    //check if in camera view
    private void CheckInCameraView()
    {
        //if in camera's viewport
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), GetComponent<Collider>().bounds))
        {
            inView = true;
        }
        else
        {
            inView = false;
        }
    }

    //track off screen
    private void TrackOffScreenTarget()
    {
        //if not in view
        if (!inView)
        {
            //get direction to target from player
            Vector3 direction = transform.position - player.transform.position;

            //get the current edge of screen x and z values
            Vector2 screenMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector2 screenMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

            //set the offset of edges of screen
            Vector2 screenBoundsMin = new Vector2(screenMin.x + imageOffsetDistance, screenMin.y + imageOffsetDistance);
            Vector2 screenBoundsMax = new Vector2(screenMax.x - imageOffsetDistance, screenMax.y - imageOffsetDistance);

            //position the indicator in the world where the target is
            indicatorObject.transform.position = transform.position;
            //clamp the x and z pos by screen bounds
            float newX = Mathf.Clamp(indicatorObject.transform.position.x, screenBoundsMin.x, screenBoundsMax.x);
            float newZ = Mathf.Clamp(indicatorObject.transform.position.z, screenBoundsMin.y, screenBoundsMax.y);
            //set new position
            indicatorObject.transform.position = new Vector3(newX, yPlane, newZ);

            //rotate to face the target
            indicatorObject.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
