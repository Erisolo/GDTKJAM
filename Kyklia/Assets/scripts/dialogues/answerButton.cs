using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class answerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    [SerializeField] int opNumber;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }
    private void OnEnable()
    {
        buttonText.color = normalColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Notify);

    }
    void Notify()
    {
        DialogueManager.instance.answerChosen(opNumber);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
