using UnityEngine.SceneManagement;

public class GBSceneManager
{
    public void Load(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}