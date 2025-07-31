using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paintButton : MonoBehaviour
{
    [SerializeField] List<paintButton> forbiddenInEditor;
    HashSet<paintButton> forbidden = new HashSet<paintButton>();
    [HideInInspector] public Color color;
    public int twistsAllowed;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        color = img.color;

        for (int i = 0; i < forbiddenInEditor.Count; i++)
        {
            forbidden.Add(forbiddenInEditor[i]);
        }

        GetComponent<Button>().onClick.AddListener(onClick);
    }

    void onClick()
    {
        paintSelector.instance.paintTaken(this);
    }

    public HashSet<paintButton> getForbidden()
    {
        return forbidden;
    }
}
