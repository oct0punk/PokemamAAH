using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public static StockManager instance;
    public Dictionary<AnimalAsset, bool> stock = new Dictionary<AnimalAsset, bool>();
    [HideInInspector] public AnimalAsset shiny;

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

    public void AddAnimal(AnimalAsset animal)
    {
        if (stock.ContainsKey(animal))
            stock[animal] = false;                    
        else
            stock.Add(animal, false);
    }

    public void Adopt(AnimalAsset asset)
    {
        if (stock.ContainsKey(asset))
            stock[asset] = true;
        else
            stock.Add(asset, true);
    }

    public bool GetAnimalValue(string assetName)
    {
        foreach (var animal in stock.Keys)
        {
            Debug.Log("----------");
            Debug.Log(animal.name);
            Debug.Log(assetName);
            if (animal.name == assetName)
                return stock[animal];
        }
        return false;
    }
}
