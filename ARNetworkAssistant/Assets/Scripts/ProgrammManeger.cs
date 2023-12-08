using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ProgrammManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject planeMarkerPrefab;
    [SerializeField]
    private Camera ARCamera;
    [SerializeField]
    private GameObject PanelChangeIP;
    [SerializeField]
    private InputPanel InputPanel;

    private ARRaycastManager ARRaycastManagerScript;

    private int touchCnt = 0;
    private float lastTouchTime = 0f;
    private float doubleTapDelay = 0.2f;

    void Start()
    {
        ARRaycastManagerScript = FindAnyObjectByType<ARRaycastManager>();
        planeMarkerPrefab.SetActive(false);
        PanelChangeIP.SetActive(false);
    }

    void Update()
    {
        ShowMarker();
        TuchCount();
    }

    private void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            planeMarkerPrefab.transform.position = hits[0].pose.position;
            planeMarkerPrefab.SetActive(true);
        }
        else
        {
            planeMarkerPrefab.SetActive(false);
        }
    }

    private void TuchCount()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTouchTime < doubleTapDelay && touchCnt == 1)
                {
                    TouchLapTop(touch.position);
                    touchCnt = 0;
                }
                else
                {
                    // Это первое касание
                    touchCnt = 1;
                    lastTouchTime = Time.time;
                }
            }
        }
    }

    private void TouchLapTop(Vector2 touchPosition)
    {
        Ray ray = ARCamera.ScreenPointToRay(touchPosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Laptop"))
            {
                if (InputPanel != null)
                {
                    Laptop laptopComponent = raycastHit.collider.gameObject.GetComponent<Laptop>();
                    if (laptopComponent != null)
                    {
                        InputPanel.Laptop = laptopComponent;
                        PanelChangeIP.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogError("InputPanel is null. Make sure it is properly initialized.");
                }
            }
        }
    }
}
