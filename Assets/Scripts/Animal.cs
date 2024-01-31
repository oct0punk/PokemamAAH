using System.Collections;
using UnityEngine;

public enum Need
{
    Hug,
    Hungry,
    Excited,
}

public class Animal : MonoBehaviour
{
    int PV;
    public Need need { get; private set; }
    public AnimalAsset asset { get; private set; }


    private new SpriteRenderer renderer;
    private Animator animator;
    [Space] 
    [SerializeField] private Transform start;
    [SerializeField] private Transform player;
    [Space]
    public ParticleSystem goodFx;
    public ParticleSystem hungryFX;
    public ParticleSystem loveFX;
    public ParticleSystem playFX;
    public ParticleSystem fuiteFX;
    Vector2 targetPos;

    private void Awake()
    {
        targetPos = transform.position;
        renderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 move = targetPos - (Vector2)transform.position;
        if (move.magnitude > .1f)
        {
            transform.position += Vector3.ClampMagnitude(move, Time.deltaTime);
        }
    }

    public void Init(AnimalAsset asset)
    {
        this.asset = asset;
        PV = 100;
        animator.runtimeAnimatorController = asset.animCon;
        ChangeNeed();
    }

    public void AddPV(int amount)
    {
        PV += amount;
        targetPos = Vector3.Lerp(player.position, start.position, PV / 100.0f);
        if (PV <= 0)
            FightManager.instance.Catch();
        else if (PV > 100)
            FightManager.instance.Escape();
        
    }

    void ChangeNeed()
    {
        int valEnum = Random.Range(0, 3);
        switch (valEnum)
        {
            case 0: 
                need = Need.Hug;
                renderer.sprite = asset.hug;
                loveFX.gameObject.SetActive(true);
                hungryFX.gameObject.SetActive(false);
                playFX.gameObject.SetActive(false);
                break;
            case 1: 
                need = Need.Hungry; 
                renderer.sprite = asset.hungry;
                loveFX.gameObject.SetActive(false);
                hungryFX.gameObject.SetActive(true);
                playFX.gameObject.SetActive(false);
                break;
            case 2: 
                need = Need.Excited; 
                renderer.sprite = asset.excited;
                loveFX.gameObject.SetActive(false);
                hungryFX.gameObject.SetActive(false);
                playFX.gameObject.SetActive(true);
                break;
        }
        animator.SetInteger("State", valEnum);
    }

    public void Hug()
    {
        StartCoroutine(TestEmotion(Need.Hug));
    }

    public void Eat()
    {
        StartCoroutine(TestEmotion(Need.Hungry));
    }

    public void Play()
    {
        StartCoroutine(TestEmotion(Need.Excited));
    }
    IEnumerator TestEmotion(Need stimuli)
    {
        yield return new WaitUntil(() => FightManager.instance.state == FightState.Reaction);
        if (need == stimuli)
        {
            goodFx.Emit(7);
            AddPV(-20);
        }
        else
        {
            AddPV(30);
            fuiteFX.Emit(1);
        }
        ChangeNeed();
        FightManager.instance.state = FightState.Standby;
    }
}
