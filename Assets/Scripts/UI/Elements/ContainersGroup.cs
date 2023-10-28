using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ContainersGroup : MonoBehaviour
    {
        public CanvasGroup[] containers;
        private RectTransform rectTransform;
        public int page;
        public LeanTweenType transition;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            for (int i = 0; i < containers.Length; i++)
            {
                CanvasGroup container = containers[i];

                if (i != page)
                {
                    container.alpha = 0;
                    container.interactable = false;
                    container.blocksRaycasts = false;
                }
            }
        }

        public void GoTo(int newPage)
        {
            LeanTween.alphaCanvas(containers[page], 0, 1f);

            containers[page].interactable = false;
            containers[page].blocksRaycasts = false;

            page = newPage;
            
            LeanTween.alphaCanvas(containers[page], 1, 1f).setDelay(1.5f);

            containers[page].interactable = true;
            containers[page].blocksRaycasts = true;
        }
    }
}