using UnityEngine.UI;
using UnityEngine;

public class GameModeInformation : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image image;

    public void SetTitle(string content) {
        title.text= content;
    }
    public void SetDescription(string content) {
        description.text = content;
    }
    public void SetImage(Sprite sprite) {
        image.sprite = sprite;
    }

    public void Set(GameMode.GameMode gameMode) {
        SetTitle(gameMode.name);
        SetDescription(gameMode.description);
        SetImage(gameMode.sprite);
    }
}