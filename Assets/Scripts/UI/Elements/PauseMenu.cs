using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Text[] texts;
    private Image panel;
    [SerializeField]
    private Color panelColor;

    private void Start()
    {
        texts = GetComponentsInChildren<Text>();
        panel = GetComponent<Image>();

        GameManager.Instance.onPause += Show;
        GameManager.Instance.onResume += Hide;
    }

    public void Show(){
       foreach (Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255f);
            text.raycastTarget = true;
        }

        panel.color = panelColor;
        panel.raycastTarget = true;
    }

    public void Hide() {
        foreach (Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            text.raycastTarget = false;
        }

        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0f);
        panel.raycastTarget = false;
    }
}
