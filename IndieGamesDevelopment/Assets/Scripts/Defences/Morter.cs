using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morter : ExplosiveDefence
{
    [Header("Specific Defence Variables")]
    [SerializeField] private Transform firePosition;
    [SerializeField] private float damageRadius;
    [SerializeField] private ContactFilter2D contactFilter = new ContactFilter2D();
    [SerializeField] private GameObject constructionGO;
    //[SerializeField] private GameObject chooseFirePosition;

    List<Collider2D> results = new List<Collider2D>();

    private Button continueToWaveButton; //to get the button click event in code

    private bool isWaiting = false;

    private void Start()
    {
        Effects = GameObject.Find("----DamageEffects----").GetComponent<DamageEffects>();
        //chooseFirePosition = GameObject.Find("----ChangeFirePosition----");
        //chooseFirePosition.SetActive(true);

        AM = GameObject.Find("----AudioManager----").GetComponent<AudioManager>();
        AS = gameObject.GetComponent<AudioSource>();

        continueToWaveButton = GameObject.Find("NextWave").GetComponent<Button>();
        continueToWaveButton.onClick.AddListener(() => activateDefence()); //listen to button click.
    }
    public void Update()
    {
        if (!isWaiting && constructionGO.activeSelf == false)
        {
            //Debug.Log("FIRE");
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        AudioClip audioToPlay = AM.getRandAudio(fire);
        AS.volume = AM.GetComponent<AudioSource>().volume;
        AS.PlayOneShot(audioToPlay);
        DTExplosive(damageRadius, new Vector2(firePosition.position.x, firePosition.position.y), contactFilter, ref results);
        Instantiate(explosionEffect, firePosition.transform);
        results.Clear();
        isWaiting = true;
        yield return new WaitForSeconds(fireRate);
        isWaiting = false;
    }
}
