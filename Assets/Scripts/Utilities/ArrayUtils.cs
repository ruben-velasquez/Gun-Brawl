using UnityEngine;

public static class ArrayUtils {
    public static GameObject[] ShuffleGameObjects(GameObject[] gameObjects) {
        for (int t = 0; t < gameObjects.Length; t++ )
        {
            GameObject tmp = gameObjects[t];
            int r = Random.Range(t, gameObjects.Length);
            gameObjects[t] = gameObjects[r];
            gameObjects[r] = tmp;
        }

        return gameObjects;
    }
}