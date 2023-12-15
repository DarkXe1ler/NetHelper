using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum Tag{

}
public class SkriptSetIp : MonoBehaviour
{
    [SerializeField]
    private Camera ARCamera;
    [SerializeField]
    private GameObject PanelChangeIP;
    [SerializeField]
    private InputPanel InputPanel;


    private int touchCnt = 0;
    private float lastTouchTime = 0f;
    private float doubleTapDelay = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        TuchCount();
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
            if (raycastHit.collider.CompareTag("Unselected") || raycastHit.collider.CompareTag("Selected"))
            {
                if (InputPanel != null)
                {
                    IPAddress laptopComponent = raycastHit.collider.gameObject.GetComponent<IPAddress>();
                    if (laptopComponent != null)
                    {
                        InputPanel.IPAdrees = laptopComponent;
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
