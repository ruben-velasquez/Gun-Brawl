using UnityEngine;

public class MatchPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBox;
    [SerializeField]
    private Skin defaultSkin;
    [SerializeField]
    private InputController.IInputController defaultController;

    private void Start() {
        if(GameManager.Instance.playerInfo.Capacity != 0) {
            int index = 0;
            foreach (PlayerInfo info in GameManager.Instance.playerInfo)
            {
                GameObject box = CreatePlayer(index);

                UI.SkinSelector skinSelector = box.GetComponentInChildren<UI.SkinSelector>();
                skinSelector.value = info.skin.id;
                skinSelector.UpdateSkin();

                UI.ControllerSelector controllerSelector = box.GetComponentInChildren<UI.ControllerSelector>();
                controllerSelector.value = info.controller.id;
                controllerSelector.UpdateController();

                index++;
            }
        }
    }

    public GameObject CreatePlayer(int index) {
        GameObject box = Instantiate(playerBox, transform);
        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        return box;
    }
    
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