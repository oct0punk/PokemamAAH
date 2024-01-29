using UnityEngine;

[CreateAssetMenu(fileName = "New animal", menuName = "Animal")]
public class AnimalAsset : ScriptableObject
{
    public string type;
    public int maxPV;
    public Sprite fighting;
    public Sprite caught;

    public int state;
    public Animation[] stateAnims;
    public Sprite[] stateButtonIcons;

}
