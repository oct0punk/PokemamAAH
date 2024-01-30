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
    [SerializeField] Bubble bubble;

    bool[] presences;
    Hideout[] hides;
    Vector3 playerPos;
    float timer = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
    }

    private void Start()
    {
        timer = UnityEngine.Random.Range(3, 10);
        if (GameManager.instance.state == GameState.Street)
            OnLevelWasLoaded(0);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = UnityEngine.Random.Range(3, 10);
            
            Hideout[] emptyHides = Array.FindAll(hides, h => !h.enabled);
            if (emptyHides.Length > 0)
            {
                emptyHides[UnityEngine.Random.Range(0, emptyHides.Length - 1)].enabled = true;
            }
        }
    }

    public void OnStreetQuit()
    {
        for (int i = 0; i < presences.Length; i++)
        {
            presences[i] = hides[i].enabled;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            enabled = true;
            hides = FindObjectsOfType<Hideout>();
            if (presences == null)
                presences = new bool[hides.Length];
            else
            {
                for (int i = 0; i < presences.Length; i++)
                {
                    hides[i].enabled = presences[i];
                }
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
        bubble.DisplayText(text);
    }
}
