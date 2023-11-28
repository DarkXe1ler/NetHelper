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

    public void SetIPAddress(string ipAddress)
    {
        if (ipAddressText != null)
        {
            ipAddressText.text = ipAddress;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
