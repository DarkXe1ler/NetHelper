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

    private PoolMono<Line> linePoolMono;

    private bool final;

    private void Start()
    {
        linePoolMono = new(linePrefab,maxLines,transform);
        linePoolMono.autoExpand = false;
    }

    private void Update()
    {
        laptops = FindObjectsOfType<Laptop>();

        var existingLines = FindObjectsOfType<Line>();
        foreach (var line in existingLines)
        {
            //Destroy(line.gameObject);
            line.gameObject.SetActive(false);
        }

        for (int i = 0; i < laptops.Length; i++)
        {
            Laptop currentLaptop = laptops[i];
            Laptop nextLaptop = (i < laptops.Length - 1) ? laptops[i + 1] : laptops[0]; // Если текущий не последний, то следующий - следующий в массиве, иначе - первый

            //Line line = Instantiate(linePrefab, transform);
            Line line = linePoolMono.GetFreeElement();
            line.StretchLine(currentLaptop.transform, nextLaptop.transform);

            if (laptops.Length == maxLines && !final)
            {
                currentLaptop.gameObject.tag = "TheEnd";
                final = true;

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
