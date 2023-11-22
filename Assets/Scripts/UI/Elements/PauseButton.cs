using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseButton : Button
{
    private GameManager gameManager;

    protected override void Start() {
        gameManager = GameManager.Instance;

        base.Start();

        Animate();

        gameManager.onPause += Animate;
        gameManager.onResume += Animate;
        gameManager.onMatchEnd += OnMatchEnd;
    }

    public void Update() {
        if(gameManager.matchEnd) return;

        bool pauseButton = Input.GetKeyUp(KeyCode.P) || (Gamepad.current != null && Gamepad.current.startButton.isPressed);

        if(pauseButton) ChangePauseState();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        ChangePauseState();
    }

    public override void OnSubmit(BaseEventData eventData) {
        ChangePauseState();
    }

    private void ChangePauseState() {

        if(gameManager.paused) {
            gameManager.Resume();
            animator.SetBool("Paused", false);
        }
        else {
            gameManager.Pause();
            animator.SetBool("Paused", true);
        }
    }

    public void Animate() {
        animator.SetBool("Paused", gameManager.paused);
    }

    public void OnMatchEnd() {
        interactable = false;
        GameManager.Instance.onPause -= Animate;
        GameManager.Instance.onResume -= Animate;
        GameManager.Instance.onMatchEnd -= OnMatchEnd;
    }
}