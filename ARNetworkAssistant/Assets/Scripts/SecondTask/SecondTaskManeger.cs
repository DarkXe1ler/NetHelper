using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTaskManeger : MonoBehaviour
{
    [SerializeField]
    private Laptop[] laptops;
    [SerializeField]
    private Modem modem;
    [SerializeField]
    private Line linePrefab;

    private int generatedLinesCount = 0;  // Счетчик сгенерированных линий
    private int maxLines = 6;  // Максимальное количество линий

    private void Update()
    {
        modem = FindObjectOfType<Modem>();
        laptops = FindObjectsOfType<Laptop>();

        // Удаляем все ранее созданные линии
        var existingLines = FindObjectsOfType<Line>();
        foreach (var line in existingLines)
        {
            generatedLinesCount = 0;
            Destroy(line.gameObject);
        }

        // Создаем линии от каждого ноутбука к модему и проверяем IP адреса
        foreach (Laptop laptop in laptops)
        {
            if (modem != null)  // Проверяем, что модем существует
            {
                if (CheckIPAddress(laptop.gameObject.GetComponent<IPAddress>().IpAddressText.text))
                {
                    Line line = Instantiate(linePrefab, transform);
                    line.gameObject.SetActive(false);
                    line.StretchLine(modem.transform, laptop.transform);
                    line.gameObject.SetActive(true);

                    generatedLinesCount++;
                }
            }
        }

        // Проверяем, достигли ли максимального количества линий
        if (generatedLinesCount >= maxLines)
        {
            // Останавливаем генерацию линий
            enabled = false;
        }
    }

    // Метод для проверки IP адреса
    private bool CheckIPAddress(string ipAddress)
    {
        // Разбиваем IP адрес на октеты
        string[] ipParts = ipAddress.Split('.');
        if (ipParts.Length == 4)
        {
            // Проверка первых двух октетов
            if (ipParts[0] == "192" && ipParts[1] == "168")
            {
                // Проверка третьего и четвертого октетов
                int octet3, octet4;
                if (int.TryParse(ipParts[2], out octet3) && int.TryParse(ipParts[3], out octet4))
                {
                    return octet3 >= 0 && octet3 <= 255 && octet4 >= 0 && octet4 <= 255;
                }
            }
        }
        return false;
    }
}
