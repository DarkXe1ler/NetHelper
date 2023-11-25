using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get;}

    private List<T> pool;

    public PoolMono(T prefab, int cnt)
    {
        this.prefab = prefab;
        container = null;
        CreatePool(cnt);
    }

    public PoolMono(T prefab, int cnt, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        CreatePool(cnt);
    }

    private void CreatePool(int cnt)
    {
        pool = new List<T>();

        for (int i = 0; i < cnt; i++)
            CreateObject();
    }

    private T CreateObject(bool isActiveDef = false)
    {
        var createdObject = Object.Instantiate(prefab,container);
        createdObject.gameObject.SetActive(isActiveDef);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(HasFreeElement(out var element))
            return element;
        if (autoExpand)
            return CreateObject(true);

        throw new System.Exception($"There is no free element in pool of type {typeof(T)}!");
    }
}
