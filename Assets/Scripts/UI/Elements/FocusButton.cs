using UnityEngine;
using UnityEngine.UI;

public class FocusButton : MonoBehaviour {
    private void Start() {
        GetComponent<Button>().Select();
    }
}