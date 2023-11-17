using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModeInformation : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image image;
    private CanvasGroup canvasGroup;


    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

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
        if(gameMode.name == title.text) return;
        StartCoroutine(SetInfoAnim(gameMode));
    }

    private IEnumerator SetInfoAnim(GameMode.GameMode gameMode) {
        LeanTween.alphaCanvas(canvasGroup, 0, 0.2f);

        yield return new WaitForSeconds(0.2f);

        SetTitle(gameMode.name);
        SetDescription(gameMode.description);
        SetImage(gameMode.sprite);

        LeanTween.alphaCanvas(canvasGroup, 1, 0.3f);
    }
}