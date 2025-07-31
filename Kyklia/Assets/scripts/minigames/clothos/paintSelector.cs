using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paintSelector : MonoBehaviour
{
    public static paintSelector instance;
    RectTransform rectTransform;
    Image img;
    [SerializeField] Vector2 offset;
    [SerializeField] List<threadButton> threadList = new List<threadButton>();
    paintButton actPaint;
    [SerializeField] 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    private void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            Input.mousePosition,
            null, // Camera is null for Screen Space - Overlay
            out mousePos
        );

        rectTransform.anchoredPosition = mousePos + offset;
    }
    public void paintTaken(paintButton paint)
    {
        if (actPaint == paint)
            actPaint = null;

        else
        {
            actPaint = paint;
            img.color = actPaint.color;
        }
    }
    public void threadPainted(threadButton thread)
    {
        if (actPaint != null)
        {
            if (actPaint == thread.getColor())
                thread.setColor(null);
            else
            {
                //see if another thread must be un-painted
                for( int i = 0; i < threadList.Count; i++ )
                {
                    if (threadList[i].getColor() == actPaint)
                    {
                        threadList[i].setColor(null);
                    }
                }

                thread.setColor(actPaint);

                //check if the combination is correct
                int t = 0;
                bool valid = true;
                while (valid && t < threadList.Count)
                {
                    if (!threadList[t].checkIfCorrect())
                        valid = false;
                    t++;
                }
                
                if(valid)
                {
                    //TODO: IMPLEMENTAR FIN DEL MINIJUEGO
                }

            }
            
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
