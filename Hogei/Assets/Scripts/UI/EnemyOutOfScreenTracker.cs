using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOutOfScreenTracker : MonoBehaviour {

    [Header("Camera")]
    [Tooltip("The main camera")]
    public Camera mainCamera;

    [Header("Image")]
    [Tooltip("Indicator object")]
    //public GameObject indicatorObject;
    public GameObject indicatorImageObject;
    [Tooltip("Indicator image")]
    public SpriteRenderer indicator;
    [Tooltip("Offset distance for image along edge of screen")]
    public float imageOffsetDistance = 3.0f;
    [Tooltip("Y plane for indicator")]
    public float yPlane = 2.0f;

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    //player ref
    private GameObject player;
    private GameObject target;

    //control vars
    private bool inView = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(playerTag);
        mainCamera = Camera.main;

        //debug
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 10));
        //sphere2.transform.position = new Vector3(screenBoundsMax.x, 1.5F, screenBoundsMax.y);
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
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), target.GetComponent<Collider>().bounds))
        {
            inView = true;
            indicator.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
        }
        else
        {
            inView = false;
            indicator.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }

    //track off screen
    private void TrackOffScreenTarget()
    {
        //if not in view
        if (!inView)
        {

            //get direction to target from player
            Vector3 direction = target.transform.position - player.transform.position;

            //get the current edge of screen x and z values
            Vector2 screenMin = new Vector2(0, 0);
            Vector2 screenMax = new Vector2(Screen.width, Screen.height);

            print("ScreenMin: " + screenMin);
            print("ScreenMax: " + screenMax);

            //print("Player world pos: " + player.transform.position);
            //print("Target world pos: " + target.transform.position);

            //get the screen pos of player and target
            Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(player.transform.position);
            Vector2 targetScreenPos = mainCamera.WorldToScreenPoint(target.transform.position);

            //print("Player screen pos: " + playerScreenPos);
            //print("Target screen pos: " + targetScreenPos);

            //convert the screen pos to gui pos
            Vector2 playerGUIPos = GUIUtility.ScreenToGUIPoint(playerScreenPos);
            Vector2 targetGUIPos = GUIUtility.ScreenToGUIPoint(targetScreenPos);

            //set the offset of edges of screen
            Vector2 screenBoundsMin = new Vector2(screenMin.x + imageOffsetDistance, screenMin.y + imageOffsetDistance);
            Vector2 screenBoundsMax = new Vector2(screenMax.x - imageOffsetDistance, screenMax.y - imageOffsetDistance);

            ////position the indicator in the world where the target is
            //transform.position = target.transform.position;

            //clamp the x and z pos by screen bounds
            float newX = Mathf.Clamp(targetScreenPos.x, screenBoundsMin.x, screenBoundsMax.x);
            float newZ = Mathf.Clamp(targetScreenPos.y, screenBoundsMin.y, screenBoundsMax.y);

            print(newX);
            print(newZ);

            //set new position
            //Vector2 inScreenPos = new Vector2(newX, newZ);
            Vector3 inWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(newX, newZ, yPlane));
            transform.position = inWorldPos;

        }
    }

    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }
}
