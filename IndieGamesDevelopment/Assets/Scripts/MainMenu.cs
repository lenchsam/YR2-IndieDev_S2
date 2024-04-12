using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject thingToToggle;


    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void ToggleUI()
    {
        if (!thingToToggle.activeInHierarchy)
        {
            thingToToggle.SetActive(true);
        }
        else
        {
            thingToToggle.SetActive(false);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
