using UnityEngine.SceneManagement;
using UnityEngine;

public class GBSceneManager : HUDManager
{
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}