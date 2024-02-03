using System;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    public static HidingManager instance;

    Hideout[] hides;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            if (instance != this)
                Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.instance.state == GameState.Street)
            OnLevelWasLoaded(2);
    }


    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            enabled = true;
            hides = FindObjectsOfType<Hideout>();

            if (StockManager.instance != null)
            {
                if (StockManager.instance.stock.Count >= 7)
                {
                    GameManager.instance.Licorne();
                    return;
                }
                foreach (AnimalAsset asset in StockManager.instance.stock.Keys)
                {
                    foreach (Hideout h in hides)
                    {
                        h.RemoveAnimal(asset);
                    }
                }
                hides = Array.FindAll(hides, h => h.enabled);
                if (hides.Length == 0) GameManager.instance.Licorne();
            }
            else
            {
                Debug.LogError("No StockManager");
            }
        }
    }

    public void DisplayText(string text)
    {
        Bubble.instance.DisplayText(text);
    }
}
