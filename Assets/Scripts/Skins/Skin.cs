using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Gun Brawl/Skin")]
public class Skin : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite image;
    public Animation.GBAnimationStack animator;
}