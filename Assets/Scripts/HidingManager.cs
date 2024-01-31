using JetBrains.Annotations;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HidingManager : MonoBehaviour
{
    public static HidingManager instance;

    Hideout[] hides;
    Vector3 playerPos;
    float timer = 0;

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
        timer = UnityEngine.Random.Range(3, 10);
        if (GameManager.instance.state == GameState.Street)
            OnLevelWasLoaded(2);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = UnityEngine.Random.Range(0, 10);
            
            Hideout[] emptyHides = Array.FindAll(hides, h => !h.enabled);
            if (emptyHides.Length > 0)
            {
                emptyHides[UnityEngine.Random.Range(0, emptyHides.Length - 1)].SetPresence();
            }
        }
    }

    public void OnStreetQuit()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            enabled = true;
            hides = FindObjectsOfType<Hideout>();
            Debug.Log(hides.Length);

            if (StockManager.instance != null)
            {
                if (StockManager.instance.stock.Count >= 8)
                {
                    GameManager.instance.Licorne();
                    return;
                }
                foreach (Hideout h in hides)
                {
                    foreach (AnimalAsset an in h.animals)
                    {
                        if (!StockManager.instance.stock.Contains(an))
                        {
                            Debug.Log(h.name + " is active dut to " + an.type);
                            h.SetPresence();
                            break;
                        }
                    }
                }
            }
            else
            {
                GameManager.instance.Licorne();
            }

            FindObjectOfType<PlayerController>().transform.position = playerPos;
        }
        else
        {
            enabled = false;
        }        
    }

    public void DisplayText(string text)
    {
        Bubble.instance.DisplayText(text);
    }
}
