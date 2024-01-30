using UnityEngine;

public class Hideout : MonoBehaviour
{
    public AnimalAsset[] animals;
    Vector2 scale;
    float timer;

    private void Start()
    {
        scale = transform.localScale;
        timer = Random.Range(4, 13);
        enabled = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range(4, 13);
            GetComponentInChildren<Animator>().SetTrigger("Hit");
            
        }
    }

    public void SetPresence()
    {
        enabled = true;
    }

    void Rummage() {
        if (enabled)
            GameManager.instance.Fight(animals[Random.Range(0, animals.Length - 1)]);
        else
        {
            HidingManager.instance.DisplayText("Oh non, il n'y a rien ici... BOUHOUHOUHOUH");
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
