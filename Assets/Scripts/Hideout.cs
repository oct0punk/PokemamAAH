using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hideout : MonoBehaviour
{
    public List<AnimalAsset> animals;
    public string str;

    float timer;


    private void Start()
    {
        timer = Random.Range(1.0f, 6.0f);
    }

    private void Update()
    {
        if (GameManager.pause) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range(2, 7);
            GetComponentInChildren<Animator>().SetTrigger("Hit");
            AudioManager.instance.Play(gameObject.name);
        }
    }

    
    public void RemoveAnimal(AnimalAsset asset)
    {
        if (enabled)
        {
            if (animals.FindIndex(a => asset) != -1)
            {
                animals.Remove(asset);
                if (animals.Count == 0)
                {
                    enabled = false;
                }
            }
        }
    }

    void Rummage() {
        AudioManager.instance.Play("bump");
        if (enabled)
        {
            StockManager stock = StockManager.instance;
            if (stock != null)
            {
                List<AnimalAsset> list = new List<AnimalAsset>();
                foreach (var a in animals)
                {

                    if (!stock.stock.ContainsKey(a))
                    {
                        GameManager.instance.Fight(a);
                        list.Add(a);
                        break;
                    }
                }

                if (list.Count == 0)
                {
                    Debug.LogError("Error while looking for animals");
                    HidingManager.instance.DisplayText(str);
                    enabled = false;
                }
            }
            else
                GameManager.instance.Fight(animals[Random.Range(0, animals.Count - 1)]);
        }
        else
        {
            HidingManager.instance.DisplayText(str);
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.pause)
            Rummage();
    }

    private void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }
}
