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
    private Laptop laptop;
    [SerializeField]
    private GameObject planeMarkerPrefab;
    [SerializeField]
    private Camera mainCamera;


    private PoolMono<Laptop> pool;
    private void Start()
    {
        pool = new PoolMono<Laptop> (laptop,poolCnt,transform);
        pool.autoExpand = autoExpand;
    }
    public void CreateLaptop()
    {
        if (planeMarkerPrefab.activeSelf)
        {
            var pos = planeMarkerPrefab.transform.position;
            var freeLaptop = pool.GetFreeElement();
            freeLaptop.transform.LookAt(mainCamera.transform);
            freeLaptop.transform.eulerAngles = new Vector3(freeLaptop.transform.eulerAngles.x, 0, -90);
            freeLaptop.transform.position = pos;
            planeMarkerPrefab.SetActive(true);
        }
    }
}