using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New animal", menuName = "Animal")]
public class AnimalAsset : ScriptableObject
{
    public string type;
    public int maxPV;
    public Sprite fighting;
    public Sprite caught;

    public RuntimeAnimatorController animCon;
    [Tooltip("Les actions possibles avec cet animal")] public Button[] stateButtons;

}
