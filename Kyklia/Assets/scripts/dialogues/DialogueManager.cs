using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] dialogueScriptableObjects actDialogo;
    [SerializeField] TMP_Text dialogueText;
    bool completado;
    int actLine;
    [SerializeField] float letterDelay = 0.05f;
    [SerializeField] List<Button> answerOptions = new List<Button>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (actDialogo != null)
        {
            actLine = 0;
            dialogueText.gameObject.SetActive(true);
            nextLine();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !actDialogo.multipleChoice)
        {
            keyPressed();
        }
       
    }

    void keyPressed()
    {
        if (completado)
        {
            if (!nextLine())    //si no hay una linea siguiente
            {                   //pasamos al siguiente diálogo (o multiple choice)
                nextDialogue(0);
            }
        }
        else
        {
            completado = true;
        }
    }
    void showDialogue()
    {
        StartCoroutine(TypeText(actDialogo.lines[actLine]));
    }

    //if theres a next line, returns true. if not, does nothing
    bool nextLine()
    {
        if (actLine >= actDialogo.lines.Count) 
            return false;
        else
        {
            showDialogue();
            actLine++;
            return true;
        }
    }

    void nextDialogue(int op)
    {
        if (actDialogo.next.Count > 0)
        {
            actDialogo = actDialogo.next[op];
            if (actDialogo.multipleChoice)
            {
                dialogueText.gameObject.SetActive(false);
                showOptions();
            }
            else if (actDialogo.lines.Count >= 1)
            {
                dialogueText.gameObject.SetActive(true);
                actLine = 0;
                nextLine();
            }
        }
        else
        {
            //TODO: FIN DE DIÁLOGOS
        }
    }

    IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        completado = false;

        for (int i = 0; i < fullText.Length; i++)
        {
            if (completado) //si lo quieren saltar antes de tiempo
            {
                dialogueText.text = fullText;
                yield break;
            }

            dialogueText.text += fullText[i];
            yield return new WaitForSeconds(letterDelay);
        }
        completado=true;
    }

    void showOptions()
    {
        if (actDialogo.choices.Count <= answerOptions.Count)
        {
            for(int i = 0; i < actDialogo.choices.Count; i++)
            {
                answerOptions[i].gameObject.SetActive(true);
                answerOptions[i].GetComponentInChildren<TMP_Text>().text = actDialogo.choices[i];
            }
        }
        else
        {
            Debug.Log("WOWOWOW TOO MANY ANSWERS");
        }
    }

    public void answerChosen(int num)
    {
        //desactivamos todos los botones
        for(int i = 0; i < answerOptions.Count; i++)
        {
            Debug.Log("button off");
            answerOptions[i].gameObject.SetActive(false);
        }

        nextDialogue(num);
    }

    private void OnDestroy()
    {
        if(instance == this)
            instance = null;
    }
}
