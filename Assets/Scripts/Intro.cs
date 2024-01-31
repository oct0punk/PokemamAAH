using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI text;
    bool next = false;

    private void Start()
    {
        StartCoroutine(routine());
    }

    IEnumerator routine()
    {
        string str = "Maman n'a pas voulu qu'on adopte un animal... Alors si c'est comme ça, je vais aller en chercher moi même !";
        string txt = "";
        foreach (char c in str)
        {
            txt += c;
            text.text = txt;
            yield return new WaitForSeconds(0.05f);
        }
            yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => next);
        GameManager.instance.Street();
    }

    public void Next()
    {
        next = true;
    }
}
