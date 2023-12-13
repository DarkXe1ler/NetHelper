using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTaskManeger : MonoBehaviour
{
    [SerializeField]
    private Laptop[] laptops;
    [SerializeField]
    private int maxLines = 6;
    [SerializeField]
    private Line linePrefab;

    private Laptop lastLaptop;

    private void Update()
    {
        laptops = FindObjectsOfType<Laptop>();
        foreach(Laptop laptop in laptops)
            laptop.gameObject.transform.GetComponent<IPAdrees>().ChangeIPAddress("");
        for (int i = 0; i < laptops.Length; i++)
        {
            if (laptops[i].PortNext != null)
            {
                lastLaptop = laptops[i];
                continue;
            }
            if (laptops[i].PortPrevious == null && i != 0)
            {
                Line line = Instantiate(linePrefab, transform);
                line.gameObject.SetActive(false);
                line.StretchLine(laptops[i].transform, lastLaptop.transform);
                lastLaptop.PortNext = line;
                laptops[i].PortPrevious = line;
                line.gameObject.SetActive(true);
                lastLaptop = laptops[i];
                break;
            }
            if (laptops[i] == laptops[laptops.Length - 1] && laptops.Length == maxLines)
            {
                Line line = Instantiate (linePrefab, transform);
                line.gameObject.SetActive(false);
                line.StretchLine(laptops[i].transform, laptops[0].transform);
                laptops[0].PortPrevious = laptops[i].PortNext = line;
                line.gameObject.SetActive(true);
                lastLaptop = laptops[i];
                break;
            }
            lastLaptop = laptops[i];
        }
    }
}
