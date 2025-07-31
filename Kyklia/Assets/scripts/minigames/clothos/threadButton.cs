using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class threadButton : MonoBehaviour
{
    Image img;
    [SerializeField] List<threadButton> touching = new List<threadButton>();
    [SerializeField] int twists;
    paintButton color;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = 0.1f;
        img.color = Color.white;

        GetComponent<Button>().onClick.AddListener(onClick);
    }

    void onClick()
    {
        paintSelector.instance.threadPainted(this);
    }

    public paintButton getColor()
    {
        return color;
    }

    public void setColor(paintButton c)
    {
        color = c;
        if (c != null)
            img.color = color.color; 
        
        else
        {
            img.color = Color.white;
        }
            
    }

    public bool checkIfCorrect()
    {
        if(color != null)
        {
            if (color.twistsAllowed < twists)
                return false;

            HashSet<paintButton> forbidden = color.getForbidden();

            int i = 0;
            bool valid = true;
            while (valid && i < touching.Count)
            {
                if (touching[i].color != null && forbidden.Contains(touching[i].color))
                    valid = false;

                i++;
            }

            return valid;
        }
        
        return false;
    }
}

