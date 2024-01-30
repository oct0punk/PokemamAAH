using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public int val;
    public GameObject Anim;

    public void Action()
    {
        StartCoroutine(ActionCoroutine());
    }

    IEnumerator ActionCoroutine()
    {
        GameObject go = Instantiate(Anim, FindObjectOfType<Canvas>().transform);
        Animator animator = go.GetComponent<Animator>();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(go);
        FightManager.instance.Action(val);
    }
}
