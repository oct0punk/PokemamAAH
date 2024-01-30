using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI bubbleText;


    public void DisplayText(string text)
    {
        gameObject.SetActive(true);
        StartCoroutine(PrintText(text));
    }

    IEnumerator PrintText(string text)
    {
        gameObject.SetActive(true);
        string txt = "";
        foreach (char c in text)
        {
            txt += c;
            bubbleText.text = txt;
            yield return new WaitForSeconds(.03f);
        }

        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
