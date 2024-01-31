using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Street,
    Fight,
    Menu,
    Intro
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public bool last = false;
    [SerializeField] AnimalAsset licorne;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            if (instance != this)
            Destroy(gameObject);
    }

    void ChangeGameState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.Street:
                break;
            case GameState.Fight:
                break;
            case GameState.Menu: break;
        }
    }

    IEnumerator LoadAndFight(AnimalAsset animal)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            HidingManager.instance.OnStreetQuit();
        }
        SceneManager.LoadScene(1);
        yield return new WaitUntil(() => SceneManager.GetSceneByBuildIndex(1).isLoaded);
        ChangeGameState(GameState.Fight);
        FightManager.instance.Init(animal);
    }

    public void Fight(AnimalAsset animal)
    {
        enabled = false;
        StopAllCoroutines();
        StartCoroutine(LoadAndFight(animal));
    }

    public void Launch()
    {
        enabled = false;
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        SceneManager.LoadScene(4);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 4);
        ChangeGameState(GameState.Intro);
        // Liste des textes
        yield return new WaitForSeconds(1.5f);
        Street();
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 2);
        enabled = true;
    }

    public void Street()
    {
        ChangeGameState(GameState.Street);
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void End()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(3);
    }

    public void Licorne()
    {
        enabled = false;
        last = true;
        Fight(licorne);
    }
}

