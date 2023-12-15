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
    private Laptop CurrentLaptop;

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
            CurrentLaptop = laptop;
            if (modem != null)  // Проверяем, что модем существует
            {
                if (CheckIPAddress(CurrentLaptop, laptop.gameObject.GetComponent<IPAddress>().IpAddressText.text))
                {
                    Line line = Instantiate(linePrefab, transform);
                    line.gameObject.SetActive(false);
                    line.StretchLine(modem.transform, laptop.transform);
                    line.gameObject.SetActive(true);

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
                    if (octet3 >= 0 && octet3 <= 255 && octet4 >= 0 && octet4 <= 255)
                    {
                        // Проверка, чтобы у каждого адреса были разные последние две цифры IP-адреса
                        foreach (Laptop otherLaptop in laptops)
                        {
                            if (otherLaptop != CurrentLaptop)
                            {
                                string otherIPAddress = otherLaptop.gameObject.GetComponent<IPAddress>().IpAddressText.text;
                                string[] otherIpParts = otherIPAddress.Split('.');
                                if (otherIpParts.Length == 4 && otherIpParts[0] == "192" && otherIpParts[1] == "168")
                                {
                                    int otherOctet3, otherOctet4;
                                    if (int.TryParse(otherIpParts[2], out otherOctet3) && int.TryParse(otherIpParts[3], out otherOctet4))
                                    {
                                        if (otherOctet3 >= 0 && otherOctet3 <= 255 && otherOctet4 >= 0 && otherOctet4 <= 255)
                                        {
                                            // Проверка, чтобы у каждого адреса были разные последние две цифры IP-адреса
                                            if (octet3 == otherOctet3 && octet4 == otherOctet4)
                                            {
                                                // Условие выполняется, если третьи или четвертые цифры разные
                                                return false;
                                            }
                                        }
                                        else return false;
                                    }
                                    else return false;
                                }
                            }
                        }
                        // Пары последних цифр уникальны, возвращаем false
                        return true;
                    }
                }
            }
        }
        return false;
    }


    // Корутина для отображения панели успеха
    private IEnumerator ShowSuccessPanel()
    {
        panelSuccess.SetActive(true);
        yield return new WaitForSeconds(successPanelDuration);
        panelSuccess.SetActive(false);
    }
}
