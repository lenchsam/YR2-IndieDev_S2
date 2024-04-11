using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject thingToToggle;


    private void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void ToggleUI()
    {
        if (thingToToggle.active)
        {
            thingToToggle.SetActive(false);
        }
        else
        {
            thingToToggle.SetActive(true);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
