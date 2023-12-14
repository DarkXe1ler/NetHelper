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
    private Laptop CurrentLaptop;

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
            CurrentLaptop = laptop;
            if (modem != null)  // ���������, ��� ����� ����������
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
        // ��������� IP ����� �� ������
        string[] ipParts = ipAddress.Split('.');
        if (ipParts.Length == 4)
        {
            // �������� ������ ���� �������
            if (ipParts[0] == "192" && ipParts[1] == "168")
            {
                // �������� �������� � ���������� �������
                int octet3, octet4;
                if (int.TryParse(ipParts[2], out octet3) && int.TryParse(ipParts[3], out octet4))
                {
                    if (octet3 >= 0 && octet3 <= 255 && octet4 >= 0 && octet4 <= 255)
                    {
                        // ��������, ����� � ������� ������ ���� ������ ��������� ��� ����� IP-������
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
                                            // ��������, ����� � ������� ������ ���� ������ ��������� ��� ����� IP-������
                                            if (octet3 == otherOctet3 && octet4 == otherOctet4)
                                            {
                                                // ������� �����������, ���� ������ ��� ��������� ����� ������
                                                return false;
                                            }
                                        }
                                        else return false;
                                    }
                                    else return false;
                                }
                            }
                        }
                        // ���� ��������� ���� ���������, ���������� false
                        return true;
                    }
                }
            }
        }
        return false;
    }


    // �������� ��� ����������� ������ ������
    private IEnumerator ShowSuccessPanel()
    {
        panelSuccess.SetActive(true);
        yield return new WaitForSeconds(successPanelDuration);
        panelSuccess.SetActive(false);
    }
}
