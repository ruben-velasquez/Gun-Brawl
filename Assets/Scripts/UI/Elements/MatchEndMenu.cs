using UnityEngine;

public abstract class MatchEndMenu : MonoBehaviour
{
    public virtual void Start() {
        GameManager.Instance.onMatchEnd += Show;
    }
    
    public abstract void Show();
    public abstract void Hide();
}