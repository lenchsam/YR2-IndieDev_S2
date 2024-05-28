using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOnScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private bool isTimeBased = false;
    private void Animation(bool isComingIn)
    {
        if (isComingIn)
        {
            //smaller to bigger
        }
        else
        {
            //bigger to smaller
        }
    }
    public void makeTextAppear(TextOnScreenScriptableObjects SO_Text)
    {
        isTimeBased = SO_Text.isTimeBased;
        Debug.Log("TEXT APPEARING");
        text.enabled = true;
        text.text = SO_Text.screenText;
        if (isTimeBased)
        {
            Invoke("makeTextDisappear", 1);
        }
    }
    private void makeTextDisappear()
    {
        text.enabled = false;
        Animation(false);
    }
}
