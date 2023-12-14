using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPAddress : MonoBehaviour
{
    private TMP_Text ipAddressText;

    public TMP_Text IpAddressText => ipAddressText;

    private void Start()
    {
        ipAddressText = transform.Find("IPAddress").GetComponent<TMP_Text>();
        GenerateAndSetIPAddress();
    }

    private void GenerateAndSetIPAddress()
    {
        string ipAddress = GenerateRandomIPAddress();
        ipAddressText.text = ipAddress;
    }

    public string GenerateRandomIPAddress()
    {
        int octet1 = Random.Range(1, 256);
        int octet2 = Random.Range(1, 256);
        int octet3 = Random.Range(1, 256);
        int octet4 = Random.Range(1, 256);

        string ipAddress = $"{octet1}.{octet2}.{octet3}.{octet4}";

        return ipAddress;
    }

    public void ChangeIPAddress(string newIPAddress)
    {
        ipAddressText.text = newIPAddress;
    }

    private void Update()
    {

    }
}
