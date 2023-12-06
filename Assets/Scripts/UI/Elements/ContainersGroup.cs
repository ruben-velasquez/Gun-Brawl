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

                if (i != GameManager.Instance.page)
                {
                    container.alpha = 0;
                    container.interactable = false;
                    container.blocksRaycasts = false;
                }
            }

            GoToInmediatly(GameManager.Instance.page);
        }

        public void GoTo(int newPage)
        {
            LeanTween.alphaCanvas(containers[page], 0, 0.3f);

            containers[page].interactable = false;
            containers[page].blocksRaycasts = false;

            page = newPage;
            
            LeanTween.alphaCanvas(containers[page], 1, 0.3f).setDelay(0.3f);

            containers[page].interactable = true;
            containers[page].blocksRaycasts = true;

            GameManager.Instance.page = page;
        }

        private void GoToInmediatly(int newPage) {
            containers[page].alpha = 0;
            containers[page].interactable = false;
            containers[page].blocksRaycasts = false;

            page = newPage;

            containers[page].alpha = 1;
            containers[page].interactable = true;
            containers[page].blocksRaycasts = true;

            GameManager.Instance.page = page;
        }
    }
}