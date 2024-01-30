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
    float timer = 666.0f;
    bool last = false;

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

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            last = true;
            enabled = false;
        }
    }

    IEnumerator LoadAndFight(AnimalAsset animal)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            HidingManager.instance.OnStreetQuit();
        }
        SceneManager.LoadScene(1);
        ChangeGameState(GameState.Fight);
        yield return new WaitUntil(() => SceneManager.GetSceneByBuildIndex(1).isLoaded);
        FightManager.instance.Init(animal);
    }

    public void Fight(AnimalAsset animal)
    {
        enabled = false;
        StopAllCoroutines();
        StartCoroutine(LoadAndFight(animal));
    }

    public void Street()
    {
        ChangeGameState(GameState.Street);
        SceneManager.LoadScene(0);
        enabled = !last;
    }

    public void Menu()
    {
        enabled = false;
        timer = 666.0f;
    }
}

