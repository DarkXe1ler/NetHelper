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
    [SerializeField]
    private GameObject panelSuccess;
    [SerializeField]
    private GameObject nextLevelButton;  // Ссылка на кнопку следующего уровня

    private float successPanelDuration = 1f;  // Длительность отображения панели успеха

    private void Update()
    {
        laptops = FindObjectsOfType<Laptop>();

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
            line.StretchLine(currentLaptop.transform, nextLaptop.transform);

            if (laptops.Length == maxLines)
            {
                currentLaptop.gameObject.tag = "TheEnd";

                if (nextLevelButton != null)
                {
                    nextLevelButton.SetActive(true);
                }

                StartCoroutine(ShowSuccessPanel());

            }
        }
    }

    private IEnumerator ShowSuccessPanel()
    {
        panelSuccess.SetActive(true);
        yield return new WaitForSeconds(successPanelDuration);
        panelSuccess.SetActive(false);
    }
}
