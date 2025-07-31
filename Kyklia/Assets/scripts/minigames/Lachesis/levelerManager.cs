using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class levelerManager : MonoBehaviour
{
    public static levelerManager Instance;

    [SerializeField] GameObject beam;
    [SerializeField] GameObject rightPlate;
    [SerializeField] GameObject leftPlate;
    [SerializeField] GameObject leftPolea;
    [SerializeField] float lowerPerKilo;
    [SerializeField] float timeToMove;

    float ogRightPlateHeight;
    float ogLeftPlateHeight;
    float ogLeftPoleaHeight;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ogLeftPlateHeight = leftPlate.transform.position.y;
        ogRightPlateHeight = rightPlate.transform.position.y;
        ogLeftPoleaHeight = leftPolea.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void weightChanged(plateLowererController p)
    {
        if(p.gameObject == rightPlate)
        {
            LeanTween.cancel(rightPlate);
            float offset = p.getTotalWeight() * lowerPerKilo;
            Vector2 futurePos = new Vector2(rightPlate.transform.position.x,  ogRightPlateHeight - offset);
            LeanTween.move(rightPlate, futurePos, timeToMove);

            //and we move the polea as well
            LeanTween.cancel(leftPolea);
            futurePos = new Vector2(leftPolea.transform.position.x, ogLeftPoleaHeight + offset);
            LeanTween.move(leftPolea, futurePos, timeToMove);

            //y ahora rotamos la barra


        }
        else
        {
            LeanTween.cancel(leftPlate);
            float offset = p.getTotalWeight() * lowerPerKilo;
            Vector2 futurePos = new Vector2(leftPlate.transform.position.x, ogLeftPlateHeight - offset);
            LeanTween.move(leftPlate, futurePos, timeToMove);
        }
    }
}
