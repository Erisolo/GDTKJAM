using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //TODO: HACER EL FINAL DE LA ESCENA
        }
    }
}
