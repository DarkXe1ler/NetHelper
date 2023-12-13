using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StretchLine(Transform start, Transform end)
    {
        if (start != null && end != null)
        {
            // Находим промежуточную позицию (например, середину между точками)
            Vector3 middlePoint = Vector3.Lerp(start.position, end.position, 0.5f);
            // Вычисляем длину между точками
            float distance = Vector3.Distance(start.position, end.position);
            // Устанавливаем позицию объекта в середину между точками
            transform.position = middlePoint;
            // Растягиваем объект вдоль оси, соединяющей две точки
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
