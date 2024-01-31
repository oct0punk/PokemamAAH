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

        AudioManager.instance.Play("fightThem");
        AudioManager.instance.Stop("villageThemfightThem");
    }

    public void Launch()
    {
        enabled = false;

        SceneManager.LoadScene(4);
        ChangeGameState(GameState.Intro);
    }

    public void Street()
    {
        ChangeGameState(GameState.Street);
        AudioManager.instance.Play("villageThem");
        AudioManager.instance.Stop("fightThem");
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.Play("villageThem");
        AudioManager.instance.Stop("fightThem");
    }

    public void End()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(3);
        AudioManager.instance.Stop("villageThem");
        AudioManager.instance.Stop("fightThem");
        AudioManager.instance.Play("EndThem");
    }

    public void Licorne()
    {
        enabled = false;
        last = true;
        Fight(licorne);
    }
}

