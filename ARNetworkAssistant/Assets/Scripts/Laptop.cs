using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Laptop : MonoBehaviour
{
    [SerializeField]
    private Line portNext;
    [SerializeField]
    private Line portPrevious;

    public Line PortNext
    {
        get => portNext;
        set
        {
            if (portNext != value)
            {
                portNext = value;
            }
        }
    }

    public Line PortPrevious
    {
        get => portPrevious;
        set
        {
            if(portNext != value)
            {
                portPrevious = value;
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
