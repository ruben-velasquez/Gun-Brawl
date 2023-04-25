using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject HUD;

    public UI.FighterUI CreateHUD() {
        Transform hudParent = GameObject.FindGameObjectWithTag("Player HUD").transform;
        return Instantiate(HUD, hudParent).GetComponent<UI.FighterUI>();
    }
}