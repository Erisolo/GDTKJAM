using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class dialogueScriptableObjects : ScriptableObject

{
    public bool multipleChoice;

    public List<string> choices;

    //si no es de múltiple elección
    public List<string> lines;

    //tantos como choices
    public List<dialogueScriptableObjects> next;
}
