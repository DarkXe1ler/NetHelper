using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject TaskPanel;


    public void Next() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void OpenSendBox() => SceneManager.LoadScene("SandBox");

    public void OpenMainMenu() => SceneManager.LoadScene("MainMenu");

    public void GoFirstTask() => SceneManager.LoadScene("FirstTask");
    public void GoSecondTask() => SceneManager.LoadScene("SecondTask");
    public void GoThirdTask() => SceneManager.LoadScene("ThirdTask");
    public void WATCHAuthorsBLYAT() => SceneManager.LoadScene("Authors");

    public void OpenTaskPanel()
    {
        MenuPanel.SetActive(false);
        TaskPanel.SetActive(true);
    }

    public void CloseTaskPanel() 
    {
        MenuPanel.SetActive(true);
        TaskPanel.SetActive(false);
    }
}
