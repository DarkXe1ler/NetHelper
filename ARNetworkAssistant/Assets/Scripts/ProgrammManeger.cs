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
    private GameObject inputFieldPrefab; // Reference to the InputField prefab
    [SerializeField]
    private GameObject buttonPrefab; // Reference to the Button prefab

    private ARRaycastManager ARRaycastManagerScript;

    void Start()
    {
        ARRaycastManagerScript = FindAnyObjectByType<ARRaycastManager>();
        planeMarkerPrefab.SetActive(false);

        // Hide the input field and button at the start
        inputFieldPrefab.SetActive(false);
        buttonPrefab.SetActive(false);
    }

    void Update()
    {
        ShowMarker();

        // Check for double tap
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Perform a raycast to check if any object is hit
            if (Physics.Raycast(ray, out hit))
            {
                Laptop laptop = hit.collider.GetComponent<Laptop>();

                // Check if the hit object is a Laptop
                if (laptop != null)
                {
                    ShowInputFieldAndButton(laptop);
                }
            }
        }
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

    private void ShowInputFieldAndButton(Laptop laptop)
    {
        // Show the input field and button
        inputFieldPrefab.SetActive(true);
        buttonPrefab.SetActive(true);

        // Set up the button click event
        Button button = buttonPrefab.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnButtonClick(laptop));

        // Position the input field and button near the laptop
        inputFieldPrefab.transform.position = laptop.transform.position + new Vector3(0, 0.1f, 0);
        buttonPrefab.transform.position = laptop.transform.position - new Vector3(0, 0.1f, 0);
    }

    private void OnButtonClick(Laptop laptop)
    {
        // Get the text from the input field
        string newIPAddress = inputFieldPrefab.GetComponent<InputField>().text;

        // Validate the IP address format
        if (IsValidIPAddress(newIPAddress))
        {
            // Change the IP address of the laptop
            laptop.ChangeIPAddress(newIPAddress);

            // Clear the text in the input field
            inputFieldPrefab.GetComponent<InputField>().text = "";

            // Hide the input field and button
            inputFieldPrefab.SetActive(false);
            buttonPrefab.SetActive(false);
        }
        else
        {
            // Highlight the input field in red for a second
            StartCoroutine(HighlightInputField());
        }
    }

    private bool IsValidIPAddress(string ipAddress)
    {
        // Implement your IP address validation logic here
        // For simplicity, you can use regular expressions or any other method
        // that suits your validation requirements.

        // Example regular expression for IPv4 address validation:
        string pattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";
        return System.Text.RegularExpressions.Regex.IsMatch(ipAddress, pattern);
    }

    private IEnumerator HighlightInputField()
    {
        // Highlight the input field in red for a second
        inputFieldPrefab.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1.0f);
        inputFieldPrefab.GetComponent<Image>().color = Color.white;
    }
}
