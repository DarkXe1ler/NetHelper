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
    private GameObject nextLevelButton;  // ������ �� ������ ���������� ������

    private int generatedLinesCount = 0;  // ������� ��������������� �����
    private int maxLines = 6;  // ������������ ���������� �����
    private float successPanelDuration = 1f;  // ������������ ����������� ������ ������

    private void Update()
    {
        modem = FindObjectOfType<Modem>();
        laptops = FindObjectsOfType<Laptop>();

        // ������� ��� ����� ��������� �����
        var existingLines = FindObjectsOfType<Line>();
        foreach (var line in existingLines)
        {
            generatedLinesCount = 0;
            Destroy(line.gameObject);
        }

        // ������� ����� �� ������� �������� � ������ � ��������� IP ������
        bool allLaptopsPassedCheck = true;
        foreach (Laptop laptop in laptops)
        {
            //CurrentLaptop = laptop;
            if (modem != null)  // ���������, ��� ����� ����������
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
                    // ���� ���� �� ���� ������� �� ������ ��������, ������������� ���� � false
                    allLaptopsPassedCheck = false;
                }
            }
        }

        // ���������, �������� �� ������������� ���������� ����� � ��� �� �������� ������ ��������
        if (generatedLinesCount >= maxLines && allLaptopsPassedCheck)
        {
            // ������������� ��������� �����
            enabled = false;

            // ������ ��� �� "TheEnd" ��� ���� ���������
            foreach (Laptop laptop in laptops)
            {
                laptop.gameObject.tag = "TheEnd";
            }
            modem.gameObject.tag = "TheEnd";

            // ���������� ������ ���������� ������
            if (nextLevelButton != null)
            {
                nextLevelButton.SetActive(true);
            }

            // ���������� ������ ������
            StartCoroutine(ShowSuccessPanel());
        }
    }

    // ����� ��� �������� IP ������
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


    // �������� ��� ����������� ������ ������
    private IEnumerator ShowSuccessPanel()
    {
        panelSuccess.SetActive(true);
        yield return new WaitForSeconds(successPanelDuration);
        panelSuccess.SetActive(false);
    }
}
