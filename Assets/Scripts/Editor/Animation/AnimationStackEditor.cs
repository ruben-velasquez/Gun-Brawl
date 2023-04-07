using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Animation;

[CustomEditor(typeof(GBAnimationStack))]
public class AnimationStackEditor : Editor {
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("animations"), true, true, true, true);

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        };

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Animations");
        };
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        GBAnimationStack animStack = (GBAnimationStack)target;

        string[] animationNames = new string[animStack.animations != null ? animStack.animations.Count : 0];

        int index = 0;

        int currentAnimIndex = 0;


        foreach (GBAnimation anim in animStack.animations)
        {
            if(anim == null) continue;
            if(anim.name == animStack.initialAnimation) currentAnimIndex = index;

            animationNames[index] = anim.name;
            index++;
        }

        if(animStack.animations.Count != 0) {
            animStack.playOnStart = EditorGUILayout.BeginToggleGroup("Initial Animation", animStack.playOnStart);
            animStack.initialAnimation = animationNames[EditorGUILayout.Popup(currentAnimIndex, animationNames)];
            EditorGUILayout.EndToggleGroup();
        }
    }
}