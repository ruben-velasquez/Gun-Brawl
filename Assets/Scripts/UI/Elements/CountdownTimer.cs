using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private Animator countdownAnimator;
    private GameManager gameManager; // Instancia del GameManager
    private Image image;
    private bool isCounting = true;
    private Coroutine countdownCoroutine;
    private float countdownTime = 4f;

    private void Awake() {
        gameManager = GameManager.Instance; // Obtener la instancia del GameManager
    }

    private void Start()
    {
        countdownAnimator = GetComponent<Animator>();
        image = GetComponent<Image>();
        countdownCoroutine = StartCoroutine(CountdownToStart());
    }

    private void Update() {
        if (gameManager.paused) {
            if (isCounting) {
                StopCoroutine(countdownCoroutine);
                countdownAnimator.speed = 0; // Pausar la animación
                isCounting = false;
            }
            image.enabled = false;
        } else {
            if (!isCounting) {
                countdownAnimator.speed = 1; // Reanudar la animación
                isCounting = true;
                countdownCoroutine = StartCoroutine(CountdownToStart());
            }
            image.enabled = true;
        }
    }

    IEnumerator CountdownToStart() {
        countdownAnimator.Play("Count");
        while (countdownTime > 0) {
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownAnimator.gameObject.SetActive(false);
    }
}