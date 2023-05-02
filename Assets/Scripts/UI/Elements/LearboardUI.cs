using UnityEngine;
using UnityEngine.UI;

public class LearboardUI : MonoBehaviour
{
    [SerializeField]
    private Image skin;
    [SerializeField]
    private Image nameSkin;
    [SerializeField]
    private Text shootsText;
    [SerializeField]
    private Text killsText;
    [SerializeField]
    private new Text name;

    // Interfaz
    [SerializeField]
    private Image shootsGraphic;
    [SerializeField]
    private Image killsGraphic;

    public void SetShoots(int shoots) {
        shootsText.text = shoots.ToString();
    }

    public void SetKills(int kills) {
        killsText.text = kills.ToString();
    }

    public void SetSkin(Sprite image) {
        skin.sprite = image;
    }

    public void SetName(string playerName) {
        name.text = playerName;
    }

    public void SetNameSkin(Sprite image) {
        nameSkin.sprite = image;
    }

    public void SetGraphics(LearboardUIStyle style) {
        shootsGraphic.sprite = style.shootsGraphic;
        killsGraphic.sprite = style.killsGraphic;
    }
}