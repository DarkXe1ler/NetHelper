using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTaskManeger : MonoBehaviour
{
    [SerializeField]
    private Modem modems;
    [SerializeField]
    private Laptop[] laptops;
    [SerializeField]
    private int maxLines = 6;
    [SerializeField]
    private GameObject 

    private void Start()
    {
        
    }

    private void Update()
    {
        modems = FindAnyObjectByType<Modem>();
        modems.gameObject.transform.GetComponent<IPAdrees>().ChangeIPAddress("");
        laptops = FindObjectsOfType<Laptop>();
        foreach(Laptop l in laptops)
        {
            l.gameObject.transform.GetComponent<IPAdrees>().ChangeIPAddress("");
        }

    }
}
