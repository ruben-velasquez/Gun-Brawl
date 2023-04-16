using UnityEngine;
using UnityEditor;
using Animation;
using System.Collections.Generic;

public static class SkinCreator
{
    public static string path = "Assets/Animations/Player/Skins";
    public static void CreateSkin(string skinName, string texturePath, GBAnimationStack baseStack) {
        // Cargamos los Sprites de la textura
        Sprite[] sprites = Resources.LoadAll<Sprite>(texturePath);
        
        // Creamos las carpetas que va a contener las animaciones
        AssetDatabase.CreateFolder(path, skinName);
        AssetDatabase.CreateFolder(path + "/" + skinName, "Weapon Attacks");

        // Instanciamos el AnimationStack
        GBAnimationStack animator = ScriptableObject.CreateInstance<GBAnimationStack>();

        animator.playOnStart = true;
        animator.initialAnimation = "Idle";

        // Convertimos los sprites a un diccionario
        Dictionary<string, Sprite> frames = GetFrames(sprites);

        // Creamos todas las animaciones
        for (int e = 0; e < SkinFramesMap.animationsNames.Length; e++)
        {
            GBAnimation animation = CreateAnimation(e, frames, baseStack);

            string animPath;

            if(animation.name.Contains("Attack")) {
                animPath = path + "/" + skinName + "/Weapon Attacks/" + animation.name.Split('_')[0] + "/" + animation.name + ".asset";
                
                if(animation.name.Contains("Horizontal")) {
                   
                    AssetDatabase.CreateFolder(path + "/" + skinName + "/Weapon Attacks", animation.name.Split('_')[0]);
                }
            } else {
                animPath = path + "/" + skinName + "/" + animation.name + ".asset";
            } 
            
            AssetDatabase.CreateAsset(animation, animPath);

            animator.AddAnimation(animation);
        }

        // Guardamos el AnimationStack
        AssetDatabase.CreateAsset(animator, path + "/" + skinName + "/" + skinName + ".asset");

        // Guardamos todos los cambios
        AssetDatabase.SaveAssets();

        // Refrescamos los archivos
        AssetDatabase.Refresh();
    }

    private static Dictionary<string, Sprite> GetFrames(Sprite[] sprites) {
        Dictionary<string, Sprite> frames = new Dictionary<string, Sprite>();
        int index = 0;

        foreach (Sprite sprite in sprites)
        {
            frames.Add(SkinFramesMap.frames[index], sprite);
            index++;
        }

        return frames;
    }

    private static GBAnimation CreateAnimation(int index, Dictionary<string, Sprite> frames, GBAnimationStack baseStack) {
        GBAnimation animation = ScriptableObject.CreateInstance<GBAnimation>();

        animation.name = SkinFramesMap.animationsNames[index];

        GBAnimation originalAnim = baseStack.GetAnimation(animation.name);

        animation.loop = originalAnim.loop;
        animation.priority = originalAnim.priority;

        for (int i = 0; i < 7; i++)
        {
            if(SkinFramesMap.animations[index, i] == null) break;
            
            Frame frame = new Frame();

            frames.TryGetValue(SkinFramesMap.animations[index, i], out frame.sprite);
            frame.time = originalAnim.frames[i].time;
            frame.actionFrame = originalAnim.frames[i].actionFrame;
            frame.exitFrame = originalAnim.frames[i].exitFrame;

            animation.AddFrame(frame);
        }

        return animation;
    }
}