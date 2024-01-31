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
        AudioManager.instance.Play("button");
    }

    public void Quit()
    {
        AudioManager.instance.Play("button");
        Application.Quit();
    }

    public void Credits()
    {
        AudioManager.instance.Play("button");
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }
}
