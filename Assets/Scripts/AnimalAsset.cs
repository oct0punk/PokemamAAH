using UnityEngine;

[CreateAssetMenu(fileName = "New animal", menuName = "Animal")]
public class AnimalAsset : ScriptableObject
{
    public string type;

    [Header("Sprite of the animal")]
    public Sprite id;
    public Sprite neutral;
    public Sprite hug;
    public Sprite hungry;
    public Sprite excited;
    public Sprite caught;

    [Header("Sprite of the buttons")]
    public Sprite hugIcon;
    public Sprite eatIcon;
    public Sprite playIcon;

    [Header("Anims of the actions")]
    public GameObject hugAnim;
    public GameObject eatAnim;
    public GameObject playAnim;
    public RuntimeAnimatorController animCon;
    [Space]
    public string intro;
}
