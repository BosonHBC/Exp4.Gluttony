using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private void Awake()
    {
        if (instance != this || instance == null)
            instance = this;
    }

    private Camera cam;
    private Vector3 worldPoint_Ref;
    public Vector3 worldPoint;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.bGameOver)
            RaycastToWorld();

        //Debug.DrawLine(transform.position, direction*100f, Color.red);
    }

    private void RaycastToWorld()
    {
        RaycastHit hit;
        Vector3 direction = (worldPoint_Ref - transform.position).normalized;
        Ray ray = new Ray(transform.transform.position, direction);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            worldPoint = hit.point;

            // Do something with the object that was hit by the raycast.
        }
    }

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        worldPoint_Ref = point;

        //GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        //GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        //GUILayout.Label("Mouse position: " + mousePos);
        //GUILayout.Label("World position: " + point.ToString("F3"));
        //GUILayout.EndArea();
    }
}
