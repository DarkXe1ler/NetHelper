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

    private Vector2 TouchPosition;

    private ARRaycastManager ARRaycastManagerScript;

    void Start()
    {
        ARRaycastManagerScript = FindAnyObjectByType<ARRaycastManager>();
        planeMarkerPrefab.SetActive(false);
        PanelChangeIP.SetActive(false);
    }

    void Update()
    {
        ShowMarker();
        TouchLapTop();
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

    private void TouchLapTop()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;

            Ray ray = ARCamera.ScreenPointToRay(TouchPosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Laptop"))
                {
                    // Проверяем, что InputPanel не null
                    if (InputPanel != null)
                    {
                        Laptop laptopComponent = raycastHit.collider.gameObject.GetComponent<Laptop>();
                        if (laptopComponent != null)
                        {
                            // Избегаем NullReferenceException, проверяя InputPanel
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
}
