using UnityEngine;

public class MatchElements : MonoBehaviour {
    public GameObject[] children;

    void Start() {
        // Deactivate all children
        foreach (GameObject child in children) {
            child.SetActive(false);
        }

        // Randomly choose one to activate
        int indexToActivate = Random.Range(0, children.Length);
        children[indexToActivate].SetActive(true);
    }
}