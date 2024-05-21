using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject thingToToggle;
    //[SerializeField] private GameObject thingToMakeInactive;

    [SerializeField] private AudioClip[] a_MenuAudioClips;
    [SerializeField] private AudioClip a_GoIntoGame;

    protected AudioManager AM;

    private Spawner spawnerScript;


    private void Start()
    {
        AM = GameObject.Find("----AudioManager----").GetComponent<AudioManager>();

        //if in level scene find the enemy spawner script;
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
    public void LoadScene(string SceneName)
    {
        //if the current scene is main menu, play the going into level scene sound
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main Menu")
            AM.playSound(a_GoIntoGame);

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
    public void NextWave()
    {
        this.gameObject.SetActive(false);
        //set all buildings as complete

    }
    public void setInactive(GameObject thingToMakeInactive)
    {
        thingToMakeInactive.SetActive(false);
    }
    public void backToMainMenu(string SceneName)
    {
        //delete all player data
        //turrtpositions.clear
        //finish health.max
        //currentwave reset
        //points reset
        //money per round reset
        LoadScene(SceneName);
    }
    public void playSound()
    {
        AM.playSound(AM.getRandAudio(a_MenuAudioClips));
    }
}
