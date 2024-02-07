using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DexStruct
{
    public Image image;
    public AnimalAsset asset;
    public Sprite pasGotcha;
    public Sprite gotcha;
}

public class Wonderdex : MonoBehaviour
{
    [SerializeField] private DexStruct[] content;
    [SerializeField] private GameObject panel;
    [SerializeField] private bool isOpen;

    private void Load()
    {
        System.Collections.Generic.Dictionary<AnimalAsset, bool> stock = StockManager.instance.stock;
        for (int i = 0; i < content.Length; i++)
        {
            DexStruct dex = content[i];
            if (stock.ContainsKey(dex.asset)) {
                if (stock[dex.asset]) {
                    dex.image.sprite = dex.gotcha;
                    dex.image.transform.SetAsFirstSibling();
                }
                else {
                    dex.image.sprite = dex.pasGotcha;
                }
            }
            else {
                dex.image.sprite = dex.pasGotcha;
            }
            content[i].image.GetComponent<RectTransform>().pivot = content[i].image.sprite.pivot;
        }
    }

    public void DexButton()
    {
        if (isOpen)
        {
            isOpen = false;
            panel.SetActive(false);
            GameManager.instance.PlayGame();
        }
        else
        {
            GameManager.instance.Pause();
            isOpen = true;
            panel.SetActive(true);
            Load();
        }
    }
}
