using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Laptop : MonoBehaviour
{
    private TMP_Text ipAddressText; // Reference to the TextMeshPro component for displaying IP address

    private void Start()
    {
        // Get the TextMeshPro component from the child named "IPAddress"
        ipAddressText = transform.Find("IPAddress").GetComponent<TMP_Text>();

        // Generate and set the IP address when the Laptop object is created
        GenerateAndSetIPAddress();
    }

    private void GenerateAndSetIPAddress()
    {
        // Generate a random IP address with the format "XXX.XXX.XXX.XXX"
        string ipAddress = GenerateRandomIPAddress();

        // Set the generated IP address to the TextMeshPro component
        ipAddressText.text = ipAddress;
    }

    private string GenerateRandomIPAddress()
    {
        // Generate four random numbers in the range 1-255 to represent each octet of the IP address
        int octet1 = Random.Range(1, 256);
        int octet2 = Random.Range(1, 256);
        int octet3 = Random.Range(1, 256);
        int octet4 = Random.Range(1, 256);

        // Combine the octets into the IP address format
        string ipAddress = $"{octet1}.{octet2}.{octet3}.{octet4}";

        return ipAddress;
    }

    private void Update()
    {
        // You can add any additional update logic here if needed
    }
}