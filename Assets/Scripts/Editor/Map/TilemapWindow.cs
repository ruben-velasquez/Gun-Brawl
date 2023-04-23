using UnityEngine;
using UnityEditor;

namespace TilemapEditor
{
    public class TilemapWindow : EditorWindow {
        public static GameObject prefab;
        public static Vector3 prefabPosition;
        public static bool isGround;
        private static Transform map;

        [MenuItem("Tilemap Editor/Editor")]
        public static void ShowWindow() {
            GetWindow<TilemapWindow>("Tilemap Editor");
        }

        private void OnGUI() {
            if(GameObject.FindGameObjectWithTag("Map") == null) {
                GUILayout.Label("No se ha encontrado el Objeto Map");
                return;
            }
            GUILayout.Label("Aquí podrás crear el escenario, pon un tile en el siguiente cuadro y haz click en el botón para ponerlo");

            GUILayout.Space(10);
            
            prefab = (GameObject)EditorGUILayout.ObjectField("Tile", prefab, typeof(GameObject), true);
            prefabPosition = EditorGUILayout.Vector3Field("Posición", prefabPosition);
            GUILayout.Label("Is Ground:");
            isGround = EditorGUILayout.Toggle(isGround);

            
            if (GUILayout.Button("Crear tile"))
            {
                map = GameObject.FindGameObjectWithTag("Map").transform;

                GameObject tile = Instantiate(prefab, prefabPosition, new Quaternion(), map);

                if(isGround) {
                    tile.AddComponent<BoxCollider2D>().usedByComposite = true;
                    tile.layer = LayerMask.NameToLayer("Ground");
                }
                

                TilemapEditor.Refresh();
            }

            if (GUILayout.Button("Refrescar vista")) {
                TilemapEditor.Refresh();
            }
        }
}
}