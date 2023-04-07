using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using Animation;

[CustomEditor(typeof(GBAnimation))]
public class AnimationEditor : Editor {
    private GBAnimation animation;
    private ReorderableList list;

    private void OnEnable()
    {
        animation = (GBAnimation)target;

        list = new ReorderableList(serializedObject, serializedObject.FindProperty("frames"), true, true, true, true);
    
        list.elementHeight = 64;

        // Función que renderiza la lista de frames
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // Campo del Sprite
             element.FindPropertyRelative("sprite").objectReferenceValue = EditorGUI.ObjectField(
                new Rect(rect.x, rect.y, 60, 60),
                element.FindPropertyRelative("sprite").objectReferenceValue,
                typeof(Sprite),
                true
            );

            // Campo de la duración
            EditorGUI.LabelField(new Rect(rect.x + 70,rect.y, 80, EditorGUIUtility.singleLineHeight), "Duration");

            element.FindPropertyRelative("time").floatValue = EditorGUI.FloatField(
                new Rect(
                    rect.x + 150, rect.y, 
                    rect.width - 150,
                    EditorGUIUtility.singleLineHeight
                ),

                element.FindPropertyRelative("time").floatValue
            );

            // Campo del Action Frame
            EditorGUI.LabelField(new Rect(rect.x + 70,rect.y + EditorGUIUtility.singleLineHeight + 10, 80, EditorGUIUtility.singleLineHeight), "Action Frame");

            element.FindPropertyRelative("actionFrame").boolValue = EditorGUI.Toggle(
                new Rect(
                    rect.x + 150, rect.y + EditorGUIUtility.singleLineHeight + 10, 
                    rect.width - 70 - 30 - 5,
                    EditorGUIUtility.singleLineHeight
                ),

                element.FindPropertyRelative("actionFrame").boolValue
            );
            
            // Campo del Exit Frame
            EditorGUI.LabelField(new Rect(rect.x + 70,rect.y + (EditorGUIUtility.singleLineHeight * 2) + 10, 80, EditorGUIUtility.singleLineHeight), "Exit Frame");

            element.FindPropertyRelative("exitFrame").boolValue = EditorGUI.Toggle(
                new Rect(
                    rect.x + 150, rect.y + (EditorGUIUtility.singleLineHeight * 2) + 10, 
                    rect.width - 70 - 30 - 5,
                    EditorGUIUtility.singleLineHeight
                ),

                element.FindPropertyRelative("exitFrame").boolValue
            );
        };

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Frames (" + animation.GetDuration() + "seg)");
        };
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}