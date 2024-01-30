using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    int stateIndex = 0;
    public int PV;
    int maxPV;

    [SerializeField] private GameObject animal;
    [SerializeField] private Transform animalStart;
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private RectTransform actionsRect;

    private AnimalAsset animalAsset;

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
        animalAsset = an;
        nameText.text = animalAsset.type;
        PV = animalAsset.maxPV;
        maxPV = PV;

        animal.GetComponentInChildren<SpriteRenderer>().sprite = animalAsset.fighting;
        animal.GetComponent<Animator>().runtimeAnimatorController = an.animCon;

        for (int i = 0; i < animalAsset.stateButtons.Length; i++)
        {
            ActionButton actBut = Instantiate(animalAsset.stateButtons[i], actionsRect).GetComponent<ActionButton>();
            actBut.val = i;
        }

        ToDate();
    }

    public void Catch()
    {
        GameManager.instance.Street();
    }

    public void Escape()
    {
        GameManager.instance.Street();
    }

    void ToDate()
    {
        stateIndex = Random.Range(0, 3);
        animal.GetComponent<Animator>().SetInteger("State", stateIndex);
        animal.GetComponent<Animator>().enabled = true;
    }

    public void Action(int val)
    {
        if (stateIndex == val)
        {
            AddPV(-10);
        }
        else
        {
            AddPV(20);
        }
        ToDate();
    }

    void AddPV(int amount)
    {
        PV += amount;
        if (PV < 1)
            Catch();
        else if (PV > maxPV)
            Escape();
        else
            animal.transform.position = Vector3.Lerp(player.position, animalStart.position, 1f * PV / maxPV);        
    }
}
