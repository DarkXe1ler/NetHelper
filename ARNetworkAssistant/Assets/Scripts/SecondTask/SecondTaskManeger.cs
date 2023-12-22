using System.Collections;
using UnityEngine;

public class SecondTaskManager : MonoBehaviour
{
    [SerializeField]
    private Laptop[] laptops;
    [SerializeField]
    private Modem modem;
    [SerializeField]
    private Line linePrefab;
    [SerializeField]
    private GameObject panelSuccess;
    [SerializeField]
    private GameObject nextLevelButton;  // Ссылка на кнопку следующего уровня

    private int generatedLinesCount = 0;  // Счетчик сгенерированных линий
    private int maxLines = 6;  // Максимальное количество линий
    private float successPanelDuration = 1f;  // Длительность отображения панели успеха

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
        bool allLaptopsPassedCheck = true;
        foreach (Laptop laptop in laptops)
        {
            //CurrentLaptop = laptop;
            if (modem != null)  // Проверяем, что модем существует
            {
                if (CheckIPAddress(/*CurrentLaptop*/ laptop, laptop.gameObject.GetComponent<IPAddress>().IpAddressText.text))
                {
                    Line line = Instantiate(linePrefab, transform);
                    //line.gameObject.SetActive(false);
                    line.StretchLine(modem.transform, laptop.transform);
                    //line.gameObject.SetActive(true);

                    generatedLinesCount++;
                }
                else
                {
                    // Если хотя бы один ноутбук не прошел проверку, устанавливаем флаг в false
                    allLaptopsPassedCheck = false;
                }
            }
        }

        // Проверяем, достигли ли максимального количества линий и все ли ноутбуки прошли проверку
        if (generatedLinesCount >= maxLines && allLaptopsPassedCheck)
        {
            // Останавливаем генерацию линий
            enabled = false;

            // Меняем тег на "TheEnd" для всех ноутбуков
            foreach (Laptop laptop in laptops)
            {
                laptop.gameObject.tag = "TheEnd";
            }
            modem.gameObject.tag = "TheEnd";

            // Активируем кнопку следующего уровня
            if (nextLevelButton != null)
            {
                nextLevelButton.SetActive(true);
            }

            // Показываем панель успеха
            StartCoroutine(ShowSuccessPanel());
        }
    }

    // Метод для проверки IP адреса
    private bool CheckIPAddress(Laptop currentLaptop, string ipAddress)
    {
        string[] ipParts = ipAddress.Split('.');
        if (CheckIPAddres(ipParts, "192", "168"))
        {
            foreach (Laptop otherLaptop in laptops)
            {
                if (otherLaptop != currentLaptop)
                {
                    string otherIPAddress = otherLaptop.gameObject.GetComponent<IPAddress>().IpAddressText.text;
                    string[] otherIpParts = otherIPAddress.Split('.');
                    if (CheckIPAddres(otherIpParts, "192", "168"))
                    {
                        if (int.Parse(ipParts[2]) == int.Parse(otherIpParts[2]) && int.Parse(ipParts[3]) == int.Parse(otherIpParts[3]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    private bool CheckIPAddres(string[] arr, string firstByte, string secondByte)
    {
        return arr.Length == 4 && arr[0] == firstByte && 
               arr[1] == secondByte && int.Parse(arr[2]) >= 0 && 
               int.Parse(arr[2]) <= 255 && int.Parse(arr[3]) >= 0 && int.Parse(arr[3]) <= 255;
    }


    // Корутина для отображения панели успеха
    private IEnumerator ShowSuccessPanel()
    {
        panelSuccess.SetActive(true);
        yield return new WaitForSeconds(successPanelDuration);
        panelSuccess.SetActive(false);
    }
}
