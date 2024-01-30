using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public GameObject Anim;

    public void Action()
    {
        StartCoroutine(ActionCoroutine());
    }

    IEnumerator ActionCoroutine()
    {
        FightManager.instance.state = FightState.Action;
        GameObject go = Instantiate(Anim, FindObjectOfType<Canvas>().transform);
        Animator animator = go.GetComponent<Animator>();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        FightManager.instance.state = FightState.Reaction;
        Destroy(go);
    }
}
