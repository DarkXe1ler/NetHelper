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

    private void Update()
    {
        laptops = FindObjectsOfType<Laptop>();
        foreach (Laptop laptop in laptops)
            laptop.gameObject.transform.GetComponent<IPAddress>().ChangeIPAddress("");

        // Удаляем все ранее созданные линии
        var existingLines = FindObjectsOfType<Line>();
        foreach (var line in existingLines)
        {
            Destroy(line.gameObject);
        }

        for (int i = 0; i < laptops.Length; i++)
        {
            Laptop currentLaptop = laptops[i];
            Laptop nextLaptop = (i < laptops.Length - 1) ? laptops[i + 1] : laptops[0]; // Если текущий не последний, то следующий - следующий в массиве, иначе - первый

            Line line = Instantiate(linePrefab, transform);
            line.gameObject.SetActive(false);
            line.StretchLine(currentLaptop.transform, nextLaptop.transform);
            line.gameObject.SetActive(true);
        }
    }
}
