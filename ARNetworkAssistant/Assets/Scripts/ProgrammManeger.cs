using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManeger : MonoBehaviour
{

    [SerializeField]
    private GameObject planeMarkerPrefab;
    [SerializeField]
    private GameObject gameObjectPrefab;

    public GameObject GameObjectPrefab
    {
        get => gameObjectPrefab;
        set
        {
            if (gameObjectPrefab != value && value != null)
            {
                gameObjectPrefab = value;
            }
        }
    }

    private ARRaycastManager ARRaycastManagerScript;

    void Start()
    {
        ARRaycastManagerScript = FindAnyObjectByType<ARRaycastManager>();
        planeMarkerPrefab.SetActive(false);
    }

    
    void Update()
    {
        ShowMarker();
    }

    private void ShowMarker()
    {
        List<ARRaycastHit> hits = new();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            planeMarkerPrefab.transform.position = hits[0].pose.position;
            planeMarkerPrefab.SetActive(true);
        }
    }

    public void CreateMyObject()
    {
        if (planeMarkerPrefab.active == true)
        {
            Instantiate(gameObjectPrefab, planeMarkerPrefab.transform.position, gameObjectPrefab.transform.rotation);
        }
    }
}
