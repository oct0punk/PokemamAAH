using System;
using UnityEngine;

public class Hideout : MonoBehaviour
{
    public AnimalAsset[] animals;
    Vector2 scale;
    float timer;
    public string str;

    private void Start()
    {
        scale = transform.localScale;
        timer = UnityEngine.Random.Range(4, 13);
        enabled = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = UnityEngine.Random.Range(4, 13);
            GetComponentInChildren<Animator>().SetTrigger("Hit");
            
        }
    }

    public void SetPresence()
    {
        // Si pas d'animaux, return;
        //bool found = false;
        //StockManager stock = StockManager.instance;
        //if (stock != null)
        //{
        //    foreach (var a in stock.stock)
        //    {
        //        if (Array.Find(animals, an => an.Equals(a)) == null)
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //}
        enabled = true;
    }

    void Rummage() {
        if (enabled)
        {
            StockManager stock = StockManager.instance;
            if (stock != null)
            {
                bool found = false;
                foreach (var a in animals)
                {

                    if (!stock.stock.Contains(a))
                    {
                        GameManager.instance.Fight(a);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    GameManager.instance.Licorne();
                }
            }
            else
                GameManager.instance.Fight(animals[UnityEngine.Random.Range(0, animals.Length - 1)]);
        }
        else
        {
            HidingManager.instance.DisplayText(str);
        }
    }

    private void OnMouseDown()
    {
        Rummage();
    }

    private void OnMouseEnter()
    {
        transform.localScale = scale * Vector3.one * 1.1f;
    }

    private void OnMouseExit()
    {
        transform.localScale = scale * Vector3.one;
    }
}
