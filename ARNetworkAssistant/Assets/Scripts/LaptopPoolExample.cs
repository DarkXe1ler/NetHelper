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
        CreateObj(poolLaptop.GetFreeElement());
    }

    public void CreateModem()
    {
        CreateObj(poolModem.GetFreeElement());
    }

    private void CreateObj(MonoBehaviour obj)
    {
        if (planeMarkerPrefab.activeSelf)
        {
            var pos = planeMarkerPrefab.transform.position;
            //var freeLaptop = poolLaptop.GetFreeElement();
            obj.transform.LookAt(mainCamera.transform);
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, 0, 0);
            obj.transform.position = pos;
            planeMarkerPrefab.SetActive(true);
        }
    }
}