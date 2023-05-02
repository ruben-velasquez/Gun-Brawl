using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Selector : MonoBehaviour
    {
        public event Action onChange;

        public int id;
        public Button leftButton;
        public Text textContent;
        public Image imageContent;

        public Image leftButtonImage;
        public Image textContentImage;
        public Image rightButtonImage;

        public Button rightButton;
        public int value;
        public int maxValue;

        [System.Serializable]
        public class SelectorView
        {
            public Sprite leftButton;
            public Sprite rightButton;
            public Sprite content;
        }

        public virtual void Start() {
            leftButton.onClick.AddListener(LeftButtonPress);
            rightButton.onClick.AddListener(RightButtonPress);
        }

        public void LeftButtonPress() {
            value -= 1;

            if(value < 0) value = maxValue;

            OnChange();
        }
        
        public void RightButtonPress() {
            value += 1;

            if(value > maxValue) value = 0;

            OnChange();
        }

        public void OnChange() {
            if(onChange != null)
                onChange();
        }

        public void ChangeView(SelectorView view) {
            leftButtonImage.sprite = view.leftButton;
            rightButtonImage.sprite = view.rightButton;
            textContentImage.sprite = view.content;
        }
    }
}