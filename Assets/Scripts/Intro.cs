using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        GameManager.instance.Street();
    }

    public void Next()
    {
        next = true;
    }
}
