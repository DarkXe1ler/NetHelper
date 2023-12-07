using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaptopPoolExample : MonoBehaviour
{
    [SerializeField]
    private int poolCnt = 3;
    [SerializeField]
    private bool autoExpand = false;
    [SerializeField]
    private Laptop laptopPrefab;
    [SerializeField]
    private GameObject planeMarkerPrefab;

    private PoolMono<Laptop> pool;

    private void Start()
    {
        pool = new PoolMono<Laptop> (laptopPrefab, poolCnt,transform);
        pool.autoExpand = autoExpand;

        GenerateAndSetIPAddresses();
    }
    private void GenerateAndSetIPAddresses()
    {
        foreach (var laptop in pool.GetPool())
        {
            string ipAddress = GenerateIPAddress();

            laptop.SetIPAddress(ipAddress);
        }
    }
    public void CreateLaptop()
    {
        if (planeMarkerPrefab.activeSelf)
        {
            var pos = planeMarkerPrefab.transform.position;
            var freeLaptop = pool.GetFreeElement();

            freeLaptop.transform.position = pos;
            planeMarkerPrefab.SetActive(true);
        }
    }

    private string GenerateIPAddress()
    {
        string ipAddress = $"{Random.Range(1, 256)}.{Random.Range(1, 256)}.{Random.Range(1, 256)}.{Random.Range(1, 256)}";
        return ipAddress;
    }
}
