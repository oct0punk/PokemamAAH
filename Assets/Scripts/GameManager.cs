using System.Collections;
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
    public static bool pause;

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
        StockManager.instance.AddAnimal(animal);

        AudioManager.instance.Play("fightThem");
        AudioManager.instance.Stop("villageThem");
    }

    public void Launch()
    {
        enabled = false;
        last = false;

        SceneManager.LoadScene(4);
        ChangeGameState(GameState.Intro);
        if (StockManager.instance != null)
            StockManager.instance.stock.Clear();
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
        AudioManager.instance.Stop("end");
        AudioManager.instance.Stop("adoption");
    }

    public void End()
    {
        StopAllCoroutines();
        AudioManager.instance.Stop("villageThem");
        AudioManager.instance.Stop("fightThem");
        if (!StockManager.instance.stock.ContainsValue(true))
        {
            Menu();
        }
        else
        {
            SceneManager.LoadScene(3);
            AudioManager.instance.Play("end");
        }
    }

    public void Licorne()
    {
        enabled = false;
        last = true;
        Fight(licorne);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause = true;
        FindObjectOfType<PlayerController>().enabled = false;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        pause = false;
        FindObjectOfType<PlayerController>().enabled = true;
    }
}

