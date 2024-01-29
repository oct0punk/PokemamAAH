using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    int stateIndex = 0;
    public int PV;

    [SerializeField] private SpriteRenderer animalSprite;
    [SerializeField] private GameObject player;
    [SerializeField] private Button[] buttons;

    private AnimalAsset animal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Catch();
    }

    public void Init(AnimalAsset an)
    {
        animal = an;
        PV = animal.maxPV;
        animalSprite.sprite = animal.fighting;
    }

    public void Catch()
    {
        GameManager.instance.Street();
    }

    void ToDate()
    {
        stateIndex = 
    }
}
