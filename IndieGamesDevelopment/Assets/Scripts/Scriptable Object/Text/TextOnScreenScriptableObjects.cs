using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextOnScreen", menuName = "ScriptableObjects/TextOnScreen")]
public class TextOnScreenScriptableObjects : ScriptableObject
{
    public string screenText;
    public int timeToShowText;
}
