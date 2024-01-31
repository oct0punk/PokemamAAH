using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public static Bubble instance;
    public TextMeshProUGUI bubbleText;

    private void Awake()
    {
        instance = this;
    }

    public void Activate()
    {
        GetComponent<Image>().enabled = true;
        bubbleText.enabled = true;
    }
    public void Deactivate()
    {
        GetComponent<Image>().enabled = false;
        bubbleText.enabled = false;
    }

    public void DisplayText(string text)
    {
        Activate();
        StartCoroutine(PrintText(text));
    }

    IEnumerator PrintText(string text)
    {
        Activate();
        string txt = "";
        foreach (char c in text)
        {
            txt += c;
            bubbleText.text = txt;
            yield return new WaitForSeconds(.03f);
        }

        yield return new WaitForSeconds(2);
        Deactivate();
    }
}
