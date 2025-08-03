using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guarrada : MonoBehaviour
{
   [SerializeField] InitialTweenController controller;

    private void OnDisable()
    {
        controller.gameObject.SetActive(true);
    }
}
