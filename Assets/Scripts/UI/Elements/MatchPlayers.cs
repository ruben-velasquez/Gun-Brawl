using UnityEngine;

public class MatchPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBox;
    [SerializeField]
    private Skin defaultSkin;
    [SerializeField]
    private InputController.IInputController defaultController;

    public void CreatePlayer() {
        GameObject box = Instantiate(playerBox, transform);
        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);
        
        int index = GameManager.Instance.CreatePlayerInfo();

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        GameManager.Instance.SetPlayerSkin(index, defaultSkin);
        GameManager.Instance.SetPlayerController(index, defaultController);
    }

    public void DeletePlayer(GameObject box) {
        int index = box.GetComponentInChildren<UI.SkinSelector>().id;
        GameManager.Instance.DeletePlayerInfo(index);
        DestroyImmediate(box);

        UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();

        foreach (var skinSelector in skinSelectors)
        {
            if(skinSelector.id > 0) skinSelector.id -= 1;
        }
        
        UI.ControllerSelector[] controllerSelectors = GetComponentsInChildren<UI.ControllerSelector>();

        foreach (var controllerSelector in controllerSelectors)
        {
            if(controllerSelector.id > 0) controllerSelector.id -= 1;
        }
    }
}