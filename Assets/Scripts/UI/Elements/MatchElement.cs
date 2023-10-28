using UnityEngine;

public abstract class MatchElement : MonoBehaviour
{
    [HideInInspector]
    public RectTransform rectTransform;
    [SerializeField]
    public bool hideOnPause;

    public virtual void Start() {
        rectTransform = GetComponent<RectTransform>();
        if(hideOnPause) {
            GameManager.Instance.onResume += Show;
            GameManager.Instance.onPause += Hide;
        }
        GameManager.Instance.onMatchEnd += Hide;
    }

    public abstract void Show();
    public abstract void Hide();
}