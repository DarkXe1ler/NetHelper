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
    [SerializeField]
    private GameObject AboutPanel;


    public void Next() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void OpenSendBox() => SceneManager.LoadScene("SandBox");

    public void OpenMainMenu() => SceneManager.LoadScene("MainMenu");

    public void GoFirstTask() => SceneManager.LoadScene("FirstTask");

    public void GoSecondTask() => SceneManager.LoadScene("SecondTask");

    public void GoQuizPanel() => SceneManager.LoadScene("ThreeTask");

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

    public void OpenAboutPanel()
    {
        MenuPanel.SetActive(false);
        AboutPanel.SetActive(true);
    }

    public void CloseAboutPanel()
    {
        MenuPanel.SetActive(true);
        AboutPanel.SetActive(false);
    }
}
