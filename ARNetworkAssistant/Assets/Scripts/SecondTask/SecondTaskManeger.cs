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
            if (modem != null)  // ���������, ��� ����� ����������
            {
                if (CheckIPAddress(laptop.gameObject.GetComponent<IPAddress>().IpAddressText.text))
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

            // ���������� ������ ������
            StartCoroutine(ShowSuccessPanel());
        }
    }

    // ����� ��� �������� IP ������
    private bool CheckIPAddress(string ipAddress)
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
                    return octet3 >= 0 && octet3 <= 255 && octet4 >= 0 && octet4 <= 255;
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
