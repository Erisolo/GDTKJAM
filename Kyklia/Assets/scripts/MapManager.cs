using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    bool tweening = true;

    [SerializeField] float distance;
    [SerializeField] float timeToTween;
    RectTransform rt;
    // Start is called before the first frame update

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    public void openMap()
    {
        gameObject.SetActive(true);
        LeanTween.moveY(rt, rt.anchoredPosition.y + distance, timeToTween).setOnComplete(() => { tweening = false; });
    }

    public void buttonClicked(string butt)
    {
       if(!tweening)
       {
            Debug.Log("buttonpressed");
            tweening = true;
            GameManager.Instance.changeScene(butt, false);
            LeanTween.moveY(rt, rt.anchoredPosition.y - distance, timeToTween).setOnComplete(() => { gameObject.SetActive(false); });
       }
       else
        {
            Debug.Log("WAIT A FUCKING SECOND");
        }
    }

    public void unlockZone(int i)
    {
        buttons[i].gameObject.SetActive(true);
    }
}
