using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openmenuingame : MonoBehaviour
{
    [SerializeField] private menuLogicIngame menuLogicIngame;
    [SerializeField] private GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (menu.active == false && Input.GetKeyDown(KeyCode.BackQuote))
        {
            menu.SetActive(true);
            menuLogicIngame.StartSelect();
        } else if (menu.active == true && Input.GetKeyDown(KeyCode.Escape)) 
            {
                menu.SetActive(true);
                menuLogicIngame.Continue();
            }
    }
}

