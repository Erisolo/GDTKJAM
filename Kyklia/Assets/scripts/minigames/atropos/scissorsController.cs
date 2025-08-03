using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scissorsController : MonoBehaviour
{
    [SerializeField]
    Transform dest;
    [SerializeField] float timeTo;
    bool inDest = false;
    [SerializeField] float offset;
    [SerializeField] GameObject fade;
    [SerializeField] float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.move(gameObject, new Vector2(transform.position.x, dest.position.y-offset), timeTo).setOnComplete(tweenFinish);
    }

    void tweenFinish()
    {
        inDest = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inDest)
        {
            if (Input.GetMouseButtonDown(0))
            {
                inDest = false;
                Invoke("notifyWin", 1.5f);
                LeanTween.alpha(fade, 1f, fadeTime);
                //pantalla en negro y sonido de tijeras
            }
        }
    }
    void notifyWin()
    {
        GameManager.Instance.addDialogueAsDone("Atropos");
        GameManager.Instance.changeScene("Atropos");
    }
}
