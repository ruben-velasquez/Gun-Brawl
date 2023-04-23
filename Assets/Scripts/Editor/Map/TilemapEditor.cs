using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TilemapEditor
{
    [InitializeOnLoad]
    public class TilemapEditor : MonoBehaviour {
        private static Transform[] tiles;

        static TilemapEditor() {
            if (GameObject.FindGameObjectWithTag("Map") == null) {
                return;
            }
            EditorApplication.hierarchyWindowChanged += Refresh;
        }
        
        [MenuItem("/Tilemap Editor/Refresh &O")]
        public static void Refresh() {
            if (GameObject.FindGameObjectWithTag("Map") == null)
            {
                return;
            }
            
            FindTiles();
            FixPositions();
        }

        static void FindTiles() {
            GameObject map = GameObject.FindGameObjectWithTag("Map");

            if(map) {
                tiles = map.GetComponentsInChildren<Transform>();
            }
        }

        static void FixPositions() {
            List<Vector3> positions = new List<Vector3>();

            foreach (Transform tile in tiles)
            {
                tile.position = new Vector3(
                    Mathf.Round(tile.position.x),
                    Mathf.Round(tile.position.y),
                    Mathf.Round(tile.position.z)
                );


                if(tile.gameObject.name.StartsWith("_")) continue;


                foreach (Vector3 position in positions)
                {
                    if(tile.position == position) {
                        tile.position = new Vector3(tile.position.x + 1, tile.position.y, tile.position.z);
                    }
                }

                if(tile) {
                    positions.Add(tile.position);
                }
            }
        }
    }
}