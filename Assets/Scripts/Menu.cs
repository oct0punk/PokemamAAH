using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject credits;

    public void Play()
    {
        GameManager.instance.Launch();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }
}
