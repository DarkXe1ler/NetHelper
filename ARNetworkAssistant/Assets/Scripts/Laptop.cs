using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private TMP_Text ipAddressText;

    private void Awake()
    {
        ipAddressText = GetComponentInChildren<TMP_Text>(true);
    }

    private void Start()
    {
        GenerateAndSetIPAddress();
    }

    private void Update()
    {
        // Add any additional update logic if needed
    }

    private void GenerateAndSetIPAddress()
    {
        string ipAddress = GenerateIPAddress();

        if (ipAddressText != null)
        {
            ipAddressText.text = ipAddress;
        }
    }

    private string GenerateIPAddress()
    {
        string ipAddress = $"{Random.Range(1, 256)}.{Random.Range(1, 256)}.{Random.Range(1, 256)}.{Random.Range(1, 256)}";
        return ipAddress;
    }
}