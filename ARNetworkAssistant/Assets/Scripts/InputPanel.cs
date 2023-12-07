using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    private Laptop currentLaptop;
    private TMP_InputField text;

    public Laptop Laptop
    {
        get => currentLaptop;
        set
        {
            if (currentLaptop != value)
            {
                currentLaptop = value;
                text.text = currentLaptop.IpAddressText.text;
            }
        }
    }

    private void Start()
    {
        text = GetComponent<TMP_InputField>();
    }
}
