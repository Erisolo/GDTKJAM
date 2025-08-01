using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    [SerializeField] string moiraToGo;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onClick);

    }
    void onClick()
    {
        GetComponentInParent<MapManager>().buttonClicked(moiraToGo);
    }
}
