using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class LaptopPoolExample : MonoBehaviour
{
    [SerializeField]
    private int poolLaptopCnt = 3;
    [SerializeField]
    private int poolModemCnt = 1;
    [SerializeField]
    private bool autoExpand = false;
    [SerializeField]
    private Laptop laptop;
    [SerializeField]
    private Modem modem;
    [SerializeField]
    private GameObject planeMarkerPrefab;
    [SerializeField]
    private Camera mainCamera;


    private PoolMono<Laptop> poolLaptop;
    private PoolMono<Modem> poolModem;

    private void Start()
    {
        poolLaptop = new PoolMono<Laptop> (laptop,poolLaptopCnt,transform);
        poolModem = new PoolMono<Modem>(modem,poolModemCnt,transform);
        poolLaptop.autoExpand = autoExpand;
        poolModem.autoExpand = autoExpand;
    }

    public void CreateLaptop()
    {
        if (planeMarkerPrefab.activeSelf)
            PlaceObj(poolLaptop.GetFreeElement());
    }

    public void CreateModem()
    {
        if (planeMarkerPrefab.activeSelf)
            PlaceObj(poolModem.GetFreeElement());
    }

    private void PlaceObj(MonoBehaviour obj)
    {
        var pos = planeMarkerPrefab.transform.position;
        obj.transform.position = pos;
        obj.transform.LookAt(mainCamera.transform);
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, 0, 0);
    }
}