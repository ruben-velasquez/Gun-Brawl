using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {
    private Image img;

    private void Start() {
        img = GetComponent<Image>();
    }

    public void UpdateLife(float newFixedLife) {
        img.fillAmount = newFixedLife;
    }
}