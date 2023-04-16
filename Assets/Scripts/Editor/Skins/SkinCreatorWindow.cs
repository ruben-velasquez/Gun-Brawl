using UnityEngine;
using UnityEditor;
using Animation;

public class SkinCreatorWindow : EditorWindow {
    private string skinName;
    private string texturePath;
    private GBAnimationStack baseStack;

    [MenuItem("Skins/Create Animations")]
    public static void ShowWindow()
    {
        GetWindow<SkinCreatorWindow>("Skin Creator");
    }

    private void OnGUI() {
        skinName = EditorGUILayout.TextField("Skin Name", skinName);
        texturePath = EditorGUILayout.TextField("Texture Path", texturePath);
        baseStack = (GBAnimationStack)EditorGUILayout.ObjectField("Base Animation Stack", baseStack, typeof(GBAnimationStack), true);
        
        if(GUILayout.Button("Create Skin")) {
            SkinCreator.CreateSkin(skinName, texturePath, baseStack);
        }
    }
}