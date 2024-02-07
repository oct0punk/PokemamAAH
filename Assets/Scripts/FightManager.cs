using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FightState
{
    Standby,
    Action,
    Reaction,
    Catch,
    Escape,
}

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    public FightState state;

    [SerializeField] private Animal animal;
    [Space]
    [SerializeField] private Image ID;
    [SerializeField] private RectTransform actionsRect;
    [Space]
    [SerializeField] private Image EatIcon;
    [SerializeField] private Image PlayIcon;
    [SerializeField] private Image HugIcon;
    [Space]
    [SerializeField] private Image CatchImage;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
    }

    public void Init(AnimalAsset an)
    {
        state = FightState.Standby;
        animal.Init(an);
        ID.sprite = an.id;
        ID.GetComponent<RectTransform>().pivot = an.id.pivot / (an.id.bounds.size * an.id.pixelsPerUnit);

        HugIcon.sprite = an.hugIcon;
        EatIcon.sprite = an.eatIcon;
        PlayIcon.sprite = an.playIcon;

        HugIcon.GetComponent<ActionButton>().Anim = an.hugAnim;
        EatIcon.GetComponent<ActionButton>().Anim = an.eatAnim;
        PlayIcon.GetComponent<ActionButton>().Anim = an.playAnim;
        CatchImage.sprite = an.caught;
        CatchImage.transform.parent.gameObject.SetActive(false);

        Bubble.instance.DisplayText(an.intro);
        AudioManager.instance.Play(an.name);
    }

    public void Catch()
    {
        AudioManager.instance.Play("gotcha");
        state = FightState.Catch;
        StockManager.instance.Adopt(animal.asset);
        StartCoroutine(Gotcha());
    }

    IEnumerator Gotcha()
    {
        CatchImage.transform.parent.gameObject.SetActive(true);
        yield return new WaitWhile(() => Input.GetMouseButton(0));
        yield return new WaitUntil(() => Input.GetMouseButton(0));

        if (GameManager.instance.last)
            GameManager.instance.End();
        else
            GameManager.instance.Street();
    }

    public void Escape()
    {
        AudioManager.instance.Stop("fightThem");
        AudioManager.instance.Play("fuite");
        StartCoroutine(EscapeRoutine());
    }    

    IEnumerator EscapeRoutine()
    {
        state = FightState.Escape;
        animal.gameObject.SetActive(false);
        Bubble.instance.DisplayText("Il s'est barré");
        yield return new WaitWhile(() => Input.GetMouseButton(0));
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        if (GameManager.instance.last)
            GameManager.instance.End();
        else
            GameManager.instance.Street();
    }
}
