using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IPChangePanel : MonoBehaviour
{
    private InputField inputField;
    private Button confirmButton;

    private Laptop targetLaptop;

    private void Start()
    {
        // ���� InputField � Button ����� �������� �������� � ����� "NewIP"
        Transform newIPTransform = FindChildWithTag(transform, "NewIP");

        if (newIPTransform != null)
        {
            inputField = newIPTransform.GetComponent<InputField>();
            confirmButton = newIPTransform.GetComponent<Button>();

            HidePanel();

            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }
        else
        {
            Debug.LogError("Object with tag 'NewIP' not found among children.");
        }
    }

    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }

    public void ShowPanel(Laptop laptop)
    {
        targetLaptop = laptop;
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        targetLaptop = null;
        gameObject.SetActive(false);
    }

    public void OnConfirmButtonClick()
    {
        string newIPAddress = inputField.text;

        if (IsValidIPAddress(newIPAddress))
        {
            targetLaptop.ChangeIPAddress(newIPAddress);
            inputField.text = "";
            HidePanel();
        }
        else
        {
            StartCoroutine(HighlightInputField());
        }
    }

    private bool IsValidIPAddress(string ipAddress)
    {
        // ���������� ���� ������ �������� IP-������
        return true; // ������ ��������, �������� �� ���� ������
    }

    private IEnumerator HighlightInputField()
    {
        // �������� InputField ������� ������ �� �������
        inputField.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1.0f);
        inputField.GetComponent<Image>().color = Color.white;
    }
}
