using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPAddressForModem : MonoBehaviour
{
    private TMP_Text ipAddressText;

    public TMP_Text IpAddressText => ipAddressText;

    private void Start()
    {
        ipAddressText = transform.Find("IPAddress").GetComponent<TMP_Text>();
        //GenerateAndSetIPAddress();
    }

    //private void GenerateAndSetIPAddress()
    //{
    //    string ipAddress = GenerateRandomIPAddress();
    //    ipAddressText.text = ipAddress;
    //}

    //private string GenerateRandomIPAddress()
    //{
    //    // ¬ыбираем случайную маску
    //    string[] masks = { "10.", "172.", "192.168." };
    //    string selectedMask = masks[Random.Range(0, masks.Length)];

    //    // √енерируем оставшиес€ октеты в пределах выбранной маски
    //    int octet2 = Random.Range(0, 256);
    //    int octet3 = Random.Range(0, 256);
    //    int octet4 = Random.Range(0, 256);

    //    string ipAddress = $"{selectedMask}{octet2}.{octet3}.{octet4}";

    //    return ipAddress;
    //}

    //public void ChangeIPAddress(string newIPAddress)
    //{
    //    ipAddressText.text = newIPAddress;
    //}

    //private void Update()
    //{

    //}
}
