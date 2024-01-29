using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Street,
    Fight,
    Menu
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

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

    public void Fight(AnimalAsset animal)
    {
        StartCoroutine(LoadAndFight(animal));
    }

    IEnumerator LoadAndFight(AnimalAsset animal)
    {
        SceneManager.LoadScene(1);
        ChangeGameState(GameState.Fight);
        yield return new WaitUntil(() => SceneManager.GetSceneByBuildIndex(1).isLoaded);
        FightManager.instance.Init(animal);
    }

    public void Menu()
    {

    }

    public void Street()
    {
        ChangeGameState(GameState.Street);
        SceneManager.LoadScene(0);
    }
}

