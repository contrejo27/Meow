using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject startScreen;

    public void HideScreens()
    {
        startScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
    }
    
    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
