using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject thingToToggle;

    private Spawner spawnerScript;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level")
            spawnerScript = GameObject.Find("----Spawner----").GetComponent<Spawner>();
    }
    private void OnLevelWasLoaded(int level)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level")
            spawnerScript = GameObject.Find("----Spawner----").GetComponent<Spawner>();
    }
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
    public void NextWave()
    {
        spawnerScript.ContinueNextWave();
        this.gameObject.SetActive(false);
    }
}
