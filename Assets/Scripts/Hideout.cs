using UnityEngine;

public class Hideout : MonoBehaviour
{
    public AnimalAsset[] animals;
    public bool presence = false;

    void Rummage() {
        if (presence)
            GameManager.instance.Fight(animals[Random.Range(0, animals.Length - 1)]);
    }

    private void OnMouseDown()
    {
        Rummage();
    }
}
