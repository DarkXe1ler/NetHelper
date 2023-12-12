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
    private Line linePrefab;

    private void Update()
    {
        modems = FindAnyObjectByType<Modem>();
        modems.gameObject.transform.GetComponent<IPAdrees>().ChangeIPAddress("");
        laptops = FindObjectsOfType<Laptop>();
        foreach(Laptop laptop in laptops)
        {
            if(laptop.Port == null)
            {
                Line line = Instantiate(linePrefab,transform);
                line.gameObject.SetActive(false);
                line.StretchLine(modems.transform, laptop.transform);
                laptop.Port = line;
                line.gameObject.SetActive(true);
            }
            laptop.gameObject.transform.GetComponent<IPAdrees>().ChangeIPAddress("");
        }
    }
}
