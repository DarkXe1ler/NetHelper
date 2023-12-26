using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panelChangeIP;

    private IPAddress currentObj;
    private TMP_InputField inputField;

    public IPAddress IPAdrees
    {
        get => currentObj;
        set
        {
            if (currentObj != value)
            {
                currentObj = value;
                inputField.text = currentObj.IpAddressText.text;
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

        currentObj.ChangeIPAddress(ipAddress);
        panelChangeIP.SetActive(false);
    }

    private bool IsValidIpAddress(string ipAddress)
    {
        string pattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        return Regex.IsMatch(ipAddress, pattern);
    }

    public void ValueChanged()
    {
        inputField.textComponent.color = Color.black;
    }

    public void Cancel()
    {
        panelChangeIP.SetActive(false);
    }

    public void DisableSelectedLaptop()
    {
        if (currentObj != null)
        {
            currentObj.gameObject.SetActive(false);
            string newIPAddress = currentObj.GenerateRandomIPAddress();

            currentObj.ChangeIPAddress(newIPAddress);
            panelChangeIP.SetActive(false);
        }
    }

    public void DisableSelectedLaptopFirstLvl()
    {
        if (currentObj != null)
        {
            currentObj.gameObject.SetActive(false);
            panelChangeIP.SetActive(false);
        }
    }
}
