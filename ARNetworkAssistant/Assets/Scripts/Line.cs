using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Line : MonoBehaviour
{

    public void StretchLine(Transform start, Transform end)
    {
        if (start != null && end != null)
        {
            Vector3 middlePoint = Vector3.Lerp(start.position, end.position, 0.5f);
            float distance = Vector3.Distance(start.position, end.position);
            transform.position = middlePoint;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance);
            Vector3 direction = end.position - start.position;
            transform.localRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogWarning("Необходимо установить точки startPoint и endPoint.");
        }
    }
}
