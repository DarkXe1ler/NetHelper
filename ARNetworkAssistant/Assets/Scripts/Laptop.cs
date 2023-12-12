using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Laptop : MonoBehaviour
{
    [SerializeField]
    private Line port;

    public Line Port
    {
        get => port;
        set
        {
            if (port != value)
            {
                port = value;
            }
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
