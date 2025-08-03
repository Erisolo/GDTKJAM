using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTweenController : MonoBehaviour
{
    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;
    [SerializeField] List<float> timesToWait = new List<float>();
    [SerializeField] List<float> timesToMove = new List<float>();
    [SerializeField] List<Vector2> positionsTop = new List<Vector2>();
    [SerializeField] List<Vector2> positionsBottom = new List<Vector2>();
    int actTween = 0;
    [SerializeField] float initialWat;
    [SerializeField] dialogueScriptableObjects dialogue;
    [SerializeField] GameObject pannel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("startTween", initialWat);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startTween()
    {
        LeanTween.move(top, positionsTop[actTween], timesToMove[actTween]).setOnComplete(endTween);
        LeanTween.move(bottom, positionsBottom[actTween], timesToMove[actTween]);
    }

    void endTween()
    {
        if (actTween < (positionsTop.Count -1))
        {
            Invoke("startTween", timesToWait[actTween]);
            actTween++;
        }
        else
        {
            if(DialogueManager.instance.isActiveAndEnabled)
                Invoke("showPanel", timesToWait[actTween]);
            else 
            {
                GameManager.Instance.changeScene("Menu");
            }
            
        }
    }

    void showPanel()
    {
        pannel.SetActive(true);
        Image image = pannel.GetComponent<Image>();
        LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) => {
            Color newColor = image.color;
            newColor.a = val;
            image.color = newColor;
        }).setOnComplete(startDialogue);
    }

    void startDialogue()
    {
        //LeanTween.color(pannel, new Color(1, 1, 1, 1), 0.2f);
        DialogueManager.instance.startDialogue(dialogue);
    }
}
