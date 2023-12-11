using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panelChangetIp;


    private Laptop currentLaptop;
    private TMP_InputField inputField;

    public Laptop Laptop
    {
        get => currentLaptop;
        set
        {
            if (currentLaptop != value)
            {
                currentLaptop = value;
                inputField.text = currentLaptop.IpAddressText.text;
            }
        }
    }

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void ConfirmIp()
    {
        string ipAddress = inputField.text;

        if (!IsValidIpAddress(ipAddress))
        {
            inputField.textComponent.color = Color.red;
            return;
        }

        //if (IsReservedIpAddress(ipAddress))
        //{
        //    Debug.LogError("Reserved IP address. Choose a different one.");
        //    return;
        //}

        currentLaptop.ChangeIPAddress(ipAddress);
        panelChangetIp.SetActive(false);
    }

    private bool IsValidIpAddress(string ipAddress)
    {
        string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        return Regex.IsMatch(ipAddress, pattern);
    }

    private bool IsReservedIpAddress(string ipAddress)
    {
        if (ipAddress == "127.0.0.1" || ipAddress.ToLower() == "localhost")
        {
            return true;
        }

        return false;
    }

    public void ValueChanget()
    {
        inputField.textComponent.color = Color.black;
    }

    public void Cancel()
    {
        panelChangetIp.SetActive(false);
    }
}