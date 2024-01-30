using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public static StockManager instance;
    public List<AnimalAsset> stock = new List<AnimalAsset>();
    [HideInInspector] public AnimalAsset shiny;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
    }

    public void Adopt(AnimalAsset asset)
    {
        stock.Add(asset);
    }
}
